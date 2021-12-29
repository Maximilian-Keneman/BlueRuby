using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueRuby
{
    public class TeamBot
    {
        private GameTable OwnerGame;
        private Team Team;

        private Player[] Teameats => OwnerGame.PlayersOf(Team);
        private Player[] podopechnie => Teameats.Where(P => P.IsBot).ToArray();
        private WorldParts.BotMaps Maps => OwnerGame.Maps;

        public TeamBot(GameTable game, Team team)
        {
            OwnerGame = game;
            Team = team;
        }
        public void GiveStrategies()
        {
            int tCount = Teameats.Length;
            int bCount = podopechnie.Length;

            if (bCount > 0)
            {
                if (tCount > bCount)
                {
                    foreach (Player pl in podopechnie)
                    {
                        pl.Bot.SetStrategy(Bot.RandomStrategy());
                        foreach (ChatForm F in Application.OpenForms.OfType<ChatForm>())
                            F.SendMesseage(pl, ChatForm.comboBox1Item.Team,
                                pl.Bot.MyStrategy switch
                                {
                                    Bot.Strategy.Warior => "Я пойду в атаку на них.",
                                    Bot.Strategy.Collector => "Я пойду копить карты.",
                                    Bot.Strategy.Sprinter => "Я пойду доставлю свой рубин.",
                                    Bot.Strategy.Jock => "Я буду копить энергию.",
                                });
                    }
                }
                else
                {
                    List<int> vs = Enumerable.Range(0, bCount).ToList().Shuffle();
                    switch (vs.Count)
                    {
                        case 1:
                            podopechnie[vs[0]].Bot.SetStrategy(Bot.Strategy.Sprinter);
                            break;
                        case 2:
                            podopechnie[vs[1]].Bot.SetStrategy(Bot.Strategy.Warior);
                            goto case 1;
                        case 3:
                            podopechnie[vs[2]].Bot.SetStrategy(Bot.Strategy.Collector);
                            goto case 2;
                        case 4:
                            podopechnie[vs[3]].Bot.SetStrategy(Bot.Strategy.Jock);
                            goto case 3;
                    }
                }
            }
        }

        public enum StrategyEvent
        {
            NoLive,
            NewRuby,
            HaveLive,
        }
        public void ChangeStrategy(Bot sender, StrategyEvent Event)
        {
            if (Teameats.Contains(sender.Owner))
            {
                Player own = sender.Owner;
                void NewSprinter()
                {
                    if (podopechnie.Length > 1)
                    {
                        (int dist, Player player) min = (100, null);
                        foreach (var pl in podopechnie)
                            if (pl != own)
                            {
                                int value = Maps.GetMap(pl.Team, pl.HaveCard<DragonKhiragir>(out _), pl.HaveCard<FiveGoldCard>(out _))[pl.TblLocation];
                                if (value < min.dist)
                                    min = (value, pl);
                            }
                        min.player.Bot.SetStrategy(Bot.Strategy.Sprinter);
                    }
                }
                switch (Event)
                {
                    case StrategyEvent.NoLive:
                        if (OwnerGame.Deck.Count > 0 && Maps.GetMap(own.Team, own.HaveCard<DragonKhiragir>(out _), own.HaveCard<FiveGoldCard>(out _))[own.TblLocation] < 10)
                            sender.SetStrategy(Bot.Strategy.Jock);
                        NewSprinter();
                        break;
                    case StrategyEvent.HaveLive:
                        sender.SetStrategy(Bot.Strategy.Warior);
                        break;
                    case StrategyEvent.NewRuby:
                        if (own.BlueRuby.Any(R => !R.IsOpen || (R.IsOpen && R.IsReal)))
                            sender.SetStrategy(Bot.Strategy.Sprinter);
                        else
                        {
                            sender.SetStrategy(Bot.Strategy.Warior);
                            NewSprinter();
                        }
                        break;
                }
            }
        }
    }

    public class Bot
    {
        public enum CardTurnCase
        {
            MoveCardDice,
            AttackCardDice
        }
        public enum PlaceTurnCase
        {
            MovePlace,
            AttackDirection,
            WizardSpawnPlace
        }
        public enum QuestionTurnCase
        {
            FortressQuestion,
            MagicTowerQuestion,
            CatacombQuestion,
            ShipQuestion,
            AttackQuestion
        }
        public static Strategy RandomStrategy()
        {
            Strategy[] strategies = Enum.GetValues(typeof(Strategy)).OfType<Strategy>().ToArray();
            return strategies[Expansion.Rnd.Next(strategies.Length)];
        }
        public enum Strategy
        {
            Warior,
            Collector,
            Sprinter,
            Jock,
        }
        public Strategy MyStrategy { get; private set; }
        public void SetStrategy(Strategy strategy) => MyStrategy = strategy;
        private (int Min, int Max) CurrentDistance = (2, 3);
        private (int Bad, int Good) CurrentLiveCount = (1, 2);
        public void SetParametrs(int minDistance, int maxDistance, int badLiveCount, int goodLiveCount)
        {
            CurrentDistance = (minDistance, maxDistance);
            CurrentLiveCount = (badLiveCount, goodLiveCount);
        }
        public int GetParametrs()
        {
            return CurrentDistance.Min + CurrentDistance.Max + CurrentLiveCount.Bad + CurrentLiveCount.Good;
        }

        private WorldParts.BotMaps GameCards => Owner.OwnerGame.Maps;

        public Player Owner { get; }
        private TeamBot Boss => Owner.Team switch
        {
            Team.Light => Owner.OwnerGame.LightBot,
            Team.Dark => Owner.OwnerGame.DarkBot,
            Team.Wizard => (Owner as Wizard).Owner.Bot.Boss
        };

        public Bot(Player owner)
        {
            Owner = owner;
            Owner.BlueRubyListChanged += (sender, e) => NeedStrategy(TeamBot.StrategyEvent.NewRuby);
            Owner.LiveChanged += (sender, e) =>
            {
                if (Owner.Live <= CurrentLiveCount.Bad)
                    NeedStrategy(TeamBot.StrategyEvent.NoLive);
                else if (Owner.Live >= CurrentLiveCount.Good)
                    NeedStrategy(TeamBot.StrategyEvent.HaveLive);
            };
        }

        private void NeedStrategy(TeamBot.StrategyEvent Event) => Boss.ChangeStrategy(this, Event);

        private static Sector.TypeSector[] NeedSwitch = new Sector.TypeSector[]
        {
            Sector.TypeSector.Catacomb,
            Sector.TypeSector.FinishDark,
            Sector.TypeSector.FinishLight,
            Sector.TypeSector.Fortress,
            Sector.TypeSector.MagicTower,
            Sector.TypeSector.Underground
        };

        public Point[] MoveAllVariants(GameTable.GameMap moveTable)
        {
            List<Point> variants = new List<Point>();
            for (int i = 0; i < moveTable.Size.Width; i++)
                for (int j = 0; j < moveTable.Size.Height; j++)
                {
                    Point loc = new Point(i, j);
                    if (moveTable[loc] > 0)
                        variants.Add(loc);
                }
            return variants.ToArray();
        }
        public Point[] MoveEndVariants(GameTable.GameMap moveTable, int dice)
        {
            return MoveAllVariants(moveTable).Where(loc => (NeedSwitch.Contains(Owner.OwnerGame[loc].Type) && loc != Owner.LastFortress && loc != Owner.LastMagicTower) || moveTable[loc] >= dice).ToArray();
        }

        private Point WariorChose(Point[] variants, bool dragon, bool gold)
        {
            (int min, int max) = CurrentDistance;
            Point TargetPlayer = GameCards.GetMap(Owner, dragon, gold).StartPoint;
            if (Owner.Team == Team.Wizard)
                return TargetPlayer;
            var attVars = new List<(Point variant, int distance)>();
            DoubleBool<int> currentDists = new DoubleBool<int>(1, true, true, 6);
            foreach (Point V in variants)
            {
                int dist = 0;
                if (V.X == TargetPlayer.X)
                    dist = Math.Abs(V.Y - TargetPlayer.Y);
                else if (V.Y == TargetPlayer.Y)
                    dist = Math.Abs(V.X - TargetPlayer.X);
                if (currentDists.GetBool(dist))//(1 <= dist && dist <= 6)
                    attVars.Add((V, dist));
            }

            var preferVars = attVars.Where(V => DoubleBool<int>.GetBool(CurrentDistance.Min, true,
                                                                        V.distance,
                                                                        true, CurrentDistance.Max))
                                                                      //(min <= V.distance && V.distance <= max)
                                    .Select(V => V.variant);
            int MapValue(Point P) => GameCards.GetMap(Owner, dragon, gold)[P];
            if (preferVars.Any())
                return preferVars.MinElement(MapValue);
            else if (attVars.Any())
                return attVars.Select(V => V.variant).MinElement(MapValue);
            else
                return variants.MinElement(MapValue);
        }
        public Point GetPlaceAnswer(PlaceTurnCase state, Point[] variants)
        {
            switch (state)
            {
                case PlaceTurnCase.MovePlace:
                {
                    bool dragon = GetCardAnswer(CardTurnCase.MoveCardDice, GameCards.GetMap(Owner.Team, false, false).StartPoint.StepCount(Owner.TblLocation));
                    bool gold = Owner.HaveCard<FiveGoldCard>(out _);
                    return MyStrategy switch
                    {
                        Strategy.Warior => WariorChose(variants, dragon, gold),
                        Strategy.Collector => variants.MinElement(P => GameCards.GetMap(WorldParts.BotMaps.Keys.Fortress, Owner.TblLocation, dragon, gold, Owner.LastFortress)[P]),
                        Strategy.Sprinter => variants.MinElement(P => GameCards.GetMap(Owner.Team, dragon, gold)[P]),
                        Strategy.Jock => Owner.Gold < 10 ?
                                         variants.MinElement(P => GameCards.GetMap(WorldParts.BotMaps.Keys.Fortress, Owner.TblLocation, dragon, false, Owner.LastFortress)[P]) :
                                         variants.MinElement(P => GameCards.GetMap(WorldParts.BotMaps.Keys.MagicTower, Owner.TblLocation, dragon, false, Owner.LastMagicTower)[P]),
                        _ => variants[Expansion.Rnd.Next()]
                    };
                }
                case PlaceTurnCase.AttackDirection:
                {
                    if (variants.Length == 1)
                        return variants[0];
                    else
                    {
                        var enemies = variants.Select(P =>
                            Enumerable.Range(1, 6).Select(I =>
                            {
                                Point S = P - Owner.TblLocation.ToSize();
                                return Owner.OwnerGame[P + new Size(S.X * I, S.Y * I)];
                            }).SelectMany(S => S.GetPlayers().Where(Pl => Owner.IsOpponent(Pl)))).ToList();
                        for (int i = 0; i < enemies.Count; i++)
                            if (enemies[i].Any(P => P.Team == Team.Wizard))
                                return variants[i];
                        var groupEnemies = enemies.Select(G => G.GroupBy(Pl => Pl.LocationSector)).ToList();
                        (int count, List<int> indices) maxEnemies = (0, new List<int>());
                        (int count, List<int> indices) maxGroupEnemies = (0, new List<int>());
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            if (groupEnemies[i].Count() == maxGroupEnemies.count)
                                maxGroupEnemies.indices.Add(i);
                            else if (groupEnemies[i].Count() > maxGroupEnemies.count)
                                maxGroupEnemies = (groupEnemies[i].Count(), new List<int>() { i });
                            if (enemies[i].Count() == maxEnemies.count)
                                maxEnemies.indices.Add(i);
                            else if (enemies[i].Count() > maxEnemies.count)
                                maxEnemies = (enemies[i].Count(), new List<int>() { i });
                        }
                        if (maxGroupEnemies.count >= 3)
                            return variants[maxGroupEnemies.indices[Expansion.Rnd.Next(maxGroupEnemies.indices.Count)]];
                        else
                            return variants[maxEnemies.indices[Expansion.Rnd.Next(maxEnemies.indices.Count)]];
                    }
                }
                case PlaceTurnCase.WizardSpawnPlace:
                    return variants.MinElement(P => GameCards.GetMap(Owner, false, false)[P]);
                default:
                    throw new Exception("error state");
            }
        }
        public DialogResult GetQuestionAnswer(QuestionTurnCase state)
        {
            return state switch
            {
                QuestionTurnCase.FortressQuestion => DialogResult.Yes,
                QuestionTurnCase.MagicTowerQuestion => MyStrategy switch
                {
                    Strategy.Sprinter => DialogResult.No,
                    _ => DialogResult.Yes
                },
                QuestionTurnCase.CatacombQuestion => MyStrategy switch
                {
                    Strategy.Warior => DialogResult.No,
                    Strategy.Collector => DialogResult.No,
                    Strategy.Jock => DialogResult.No,
                    Strategy.Sprinter => DialogResult.Yes,
                    _ => DialogResult.Yes
                },
                QuestionTurnCase.ShipQuestion => DialogResult.No,
                QuestionTurnCase.AttackQuestion => DialogResult.Yes,
                _ => throw new Exception("error state"),
            };
        }
        public bool GetCardAnswer(CardTurnCase state, int analizeCount)
        {
            return state switch
            {
                CardTurnCase.MoveCardDice => MyStrategy switch
                {
                    Strategy.Sprinter => Owner.HaveCard<DragonKhiragir>(out int count) && (analizeCount <= 10 || count > 1),
                    _ => Owner.HaveCard<DragonKhiragir>(out _) && true,
                },
                CardTurnCase.AttackCardDice => analizeCount <= 3,
                _ => throw new Exception("error state"),
            };
        }
    }
}
