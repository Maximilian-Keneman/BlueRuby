using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueRuby
{
    public partial class GameTable
    {
        public class Turn
        {
            private Localization Localization => MainForm.GetMainForm.Settings.Localization;
            private Dictionary<string, Sound> Sounds => GameForm.GetGameForm.Sounds;
            private GameTable Game => TurnPlayer.OwnerGame;
            private DiceForm DF;
            private Player TurnPlayer = null;
            public string TurnPlayerName => TurnPlayer.Name;

            private GameMap PlayerPathMap = null;
            private int PlayerDiceCheck = 0;
            private int PlayerFullDiceCheck = 0;
            private bool PlayerSelectWizardSector = false;
            private bool WizardTurned = false;
            private bool PlayerUseCard = false;
            private bool PlayerIsAttack = false;
            private int AttackDirection = -1;

            public static event EventHandler<TurnEventArgs> TurnPlayerChanged;
            public class TurnEventArgs : EventArgs
            {
                public Player TurnPlayer { get; }
                public Player[] LosePlayers { get; }
                public Team Team => TurnPlayer.Team;

                public TurnEventArgs(Player player, Player[] losePlayers)
                {
                    TurnPlayer = player;
                    LosePlayers = losePlayers;
                }
            }

            public static event EventHandler TurnBegun;
            private bool _isTurnStart;
            public bool IsTurnStart
            {
                get => _isTurnStart;
                private set
                {
                    _isTurnStart = value;
                    TurnBegun?.Invoke(this, EventArgs.Empty);
                }
            }
            public void TurnStart() => IsTurnStart = true;

            public Turn(Player player, DiceForm form)
            {
                DF = form;
                TurnPlayer = player;
                TurnPlayerChanged?.Invoke(Game, new TurnEventArgs(TurnPlayer, Game.LosePlayers));
                IsTurnStart = false;
                if (!TurnPlayer.LoseTurn)
                {
                    if (TurnPlayer.AtCatacomb && (TurnPlayer.SendMessage(Localization[LocalizationKeys.CatacombQuestion],
                                                                         Localization[LocalizationKeys.Interactives].Split('_')[2],
                                                                         Bot.QuestionTurnCase.CatacombQuestion,
                                                                         MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        if (TurnPlayer.Gold >= 20 && (TurnPlayer.SendMessage(Localization[LocalizationKeys.CatacombMoneyQuestion],
                                                                             Localization[LocalizationKeys.Interactives].Split('_')[2],
                                                                             Bot.QuestionTurnCase.CatacombQuestion,
                                                                             MessageBoxButtons.YesNo) == DialogResult.Yes))
                        {
                            TurnPlayer.UseGold(20);
                            TurnPlayer.InCatacomb();
                            if (TurnPlayer.IsBot)
                                DF.FastResult(DiceForm.ActiveType.FromCatacomb, 6);
                            else
                                Game.SectorsSelect(Game.Catacombs());
                        }
                        else
                            DF.CatacombDiceActivate(DiceForm.ActiveType.InCatacomb, TurnPlayer);
                    }
                    else if (TurnPlayer.InUnderground)
                        DF.CatacombDiceActivate(DiceForm.ActiveType.FromCatacomb, TurnPlayer);
                    else
                        DF.MoveDiceActivate(TurnPlayer.HaveCard<DragonKhiragir>(out _), TurnPlayer);
                        //DF.DiceActivate(DiceForm.ActiveType.Move, TurnPlayer.HaveCard<DragonKhiragir>(out _), TurnPlayer);
                }
            }
            public async Task<Turn> GoToMove(int DiceCheck, bool UseCard)
            {
                PlayerUseCard = UseCard;
                if (PlayerFullDiceCheck == 0)
                    PlayerFullDiceCheck = DiceCheck;
                if (TurnPlayer.LocationSector.Type == Sector.TypeSector.DifficultStandart && PlayerFullDiceCheck % 2 == 0)
                    return LoseTurn();
                PlayerPathMap = Game.PlayerCanGo(TurnPlayer, DiceCheck, PlayerFullDiceCheck, UseCard);
                if (Game.NoMoveCheck(PlayerPathMap))
                    return LoseTurn();
                PlayerDiceCheck = DiceCheck;
                if (TurnPlayer.IsBot)
                {
                    var variants = TurnPlayer.Bot.MoveAllVariants(PlayerPathMap);//, DiceCheck);
                    Point answer = TurnPlayer.Bot.GetPlaceAnswer(Bot.PlaceTurnCase.MovePlace, variants);
                    return await SectorSelected(Game[answer]);
                }
                else
                {
                    Game.SectorsSelect(TurnPlayer, PlayerPathMap);
                    return this;
                }
            }
            public async Task<Turn> Attack(int DiceCheck, bool UseCard)
            {
                PlayerUseCard = UseCard;
                Size[] Dir = AttackDirection switch
                {
                    0 => Enumerable.Range(1, DiceCheck).Select(i => new Size(-i, 0)).ToArray(),
                    1 => Enumerable.Range(1, DiceCheck).Select(i => new Size(0, i)).ToArray(),
                    2 => Enumerable.Range(1, DiceCheck).Select(i => new Size(i, 0)).ToArray(),
                    3 => Enumerable.Range(1, DiceCheck).Select(i => new Size(0, -i)).ToArray()
                };
                var AttackSectors = Enumerable.Range(0, DiceCheck).Select(i => TurnPlayer.TblLocation + Dir[i])
                                                                  .Where(S => S.X > 0 && S.X < Game.SectorsSize.Width &&
                                                                              S.Y > 0 && S.Y < Game.SectorsSize.Height)
                                                                  .Select(S => Game[S]).ToArray();
                if (!PlayerUseCard)
                    AttackSectors = new Sector[] { AttackSectors.Last() };
                Sounds["Attack"].Play();
                Task anim = Game.AttackAnimation(AttackSectors);
                for (int i = 0; i < AttackSectors.Length; i++)
                    foreach (Player player in AttackSectors[i].GetPlayers().Where(P => TurnPlayer.IsOpponent(P) && !P.InUnderground))
                        player.Damage();
                await anim;
                if (!PlayerUseCard)
                    await Task.Delay(500);
                Sounds["Attack"].Stop();
                Game.ClearAttackedSectors();
                if (PlayerUseCard)
                    TurnPlayer.UseCard<SpellMagicFire>();
                return End();
            }
            public Turn GoOnWater()
            {
                Size[] NextSectors = new Size[]
                {
                    new Size(-1, 0),
                    new Size(0, 1),
                    new Size(1, 0),
                    new Size(0, -1)
                };
                var WaterSectors = NextSectors.Select(S => Game[TurnPlayer.TblLocation + S])
                                              .Where(S => S.Type == Sector.TypeSector.Water);
                List<Point> variants = new List<Point>();
                foreach (var sector in WaterSectors)
                {
                    var finish = WorldParts.GetWaterPath(sector.TblLocation)
                                           .SelectMany(F => NextSectors.Select(S =>
                                                                       {
                                                                           if ((F + S).X >= 0 && (F + S).X < Game.SectorsSize.Width &&
                                                                               (F + S).Y >= 0 && (F + S).Y < Game.SectorsSize.Height)
                                                                               return Game[F + S];
                                                                           else
                                                                               return null;
                                                                       })
                                                                       .Where(S => S?.Type == Sector.TypeSector.Ship)
                                                                       .Select(S => S.TblLocation));
                    variants.AddRange(finish);
                }
                if (TurnPlayer.IsBot)
                {
                    Point answer = TurnPlayer.Bot.GetPlaceAnswer(Bot.PlaceTurnCase.MovePlace, variants.ToArray());
                    return SectorSelected(Game[answer]).Result;
                }
                else
                {
                    Game.SectorsSelect(variants.ToArray());
                    return this;
                }
            }
            public Turn GoToCatacomb(int DiceCheck)
            {
                if (DiceCheck > 4)
                {
                    TurnPlayer.InCatacomb();
                    return End();
                }
                else
                    return LoseTurn();
            }
            public Turn GoFromCatacomb(int DiceCheck)
            {
                if (DiceCheck > 4)
                {
                    Point[] catacombs = Game.Catacombs();
                    if (TurnPlayer.IsBot)
                    {
                        Point answer = TurnPlayer.Bot.GetPlaceAnswer(Bot.PlaceTurnCase.MovePlace, catacombs);
                        return SectorSelected(Game[answer]).Result;
                    }
                    else
                    {
                        Game.SectorsSelect(catacombs);
                        return this;
                    }
                }
                else
                    return LoseTurn();
            }
            private Sector[] PathCreate(Sector ClickSector, out int StepCount)
            {
                Point fin = ClickSector.TblLocation;
                if (ClickSector.Type == Sector.TypeSector.Ford || ClickSector.Type == Sector.TypeSector.GoldBridge)
                {
                    if (PlayerPathMap[fin.X - 1, fin.Y] > PlayerPathMap[fin.X + 1, fin.Y])
                    {
                        StepCount = PlayerPathMap[fin.X - 1, fin.Y];
                        ClickSector = Game[fin.X - 1, fin.Y];
                    }
                    else
                    {
                        StepCount = PlayerPathMap[fin.X + 1, fin.Y];
                        ClickSector = Game[fin.X + 1, fin.Y];
                    }
                }
                else
                {
                    StepCount = PlayerPathMap[fin];
                }
                Sector[] path = new Sector[StepCount + 1];
                path[StepCount] = ClickSector;
                bool BridgeCalculated = false;
                for (int i = StepCount - 1; i >= 0; i--)
                {
                    Point CheckPoint = path[i + 1].TblLocation;
                    List<Point> CheckList = new List<Point>();
                    if (CheckPoint.X < PlayerPathMap.Size.Width)
                        CheckList.Add(new Point(CheckPoint.X + 1, CheckPoint.Y));
                    if (CheckPoint.Y < PlayerPathMap.Size.Height)
                        CheckList.Add(new Point(CheckPoint.X, CheckPoint.Y + 1));
                    if (CheckPoint.X > 0)
                        CheckList.Add(new Point(CheckPoint.X - 1, CheckPoint.Y));
                    if (CheckPoint.Y > 0)
                        CheckList.Add(new Point(CheckPoint.X, CheckPoint.Y - 1));
                    if (CheckList.Count == 0)
                        throw new Exception("Not found next path point");
                    bool PathFinded = false;
                    foreach (Point p in CheckList)
                        if (PlayerPathMap[p] == i)
                        {
                            path[i] = Game[p];
                            PathFinded = true;
                            break;
                        }
                    if (!PathFinded)
                    {
                        if (path[i + 1].Type == Sector.TypeSector.Bridge && !BridgeCalculated)
                        {
                            Point[] addPath;
                            if (Game[CheckPoint.X, CheckPoint.Y - 3].Type == Sector.TypeSector.Bridge)
                                addPath = new Point[2]
                                {
                                        new Point(CheckPoint.X, CheckPoint.Y - 2),
                                        new Point(CheckPoint.X, CheckPoint.Y - 1)
                                };
                            else if (Game[CheckPoint.X, CheckPoint.Y + 3].Type == Sector.TypeSector.Bridge)
                                addPath = new Point[2]
                                {
                                        new Point(CheckPoint.X, CheckPoint.Y + 2),
                                        new Point(CheckPoint.X, CheckPoint.Y + 1)
                                };
                            else if (Game[CheckPoint.X - 1, CheckPoint.Y + 3].Type == Sector.TypeSector.Bridge)
                                addPath = new Point[3]
                                {
                                        new Point(CheckPoint.X - 1, CheckPoint.Y + 2),
                                        new Point(CheckPoint.X, CheckPoint.Y + 2),
                                        new Point(CheckPoint.X, CheckPoint.Y + 1)
                                };
                            else if (Game[CheckPoint.X + 1, CheckPoint.Y - 3].Type == Sector.TypeSector.Bridge)
                                addPath = new Point[3]
                                {
                                        new Point(CheckPoint.X + 1, CheckPoint.Y - 2),
                                        new Point(CheckPoint.X + 1, CheckPoint.Y - 1),
                                        new Point(CheckPoint.X, CheckPoint.Y - 1)
                                };
                            else
                                throw new Exception("Not found other bridge point");
                            path = path.IncreasingArray(addPath.Length, false);
                            for (int k = 0; k < addPath.Length; k++)
                                path[i + k + 1] = Game[addPath[k]];
                            i++;
                            BridgeCalculated = true;
                        }
                    }
                }
                return path;
            }
            public async Task<Turn> SectorSelected(Sector ClickSector)
            {
                if (PlayerIsAttack)
                {
                    AttackDirection = (TurnPlayer.TblLocation, ClickSector.TblLocation).GoDir().g1;
                    DF.AttackDiceActivate(TurnPlayer.HaveCard<SpellMagicFire>(out _), TurnPlayer, AttackDirection);
                    //DF.DiceActivate(DiceForm.ActiveType.Attack, TurnPlayer.HaveCard<SpellMagicFire>(out _), TurnPlayer, AttackDirection);
                    return this;
                }
                else if (TurnPlayer.Aboard)
                {
                    TurnPlayer.FormShip(ClickSector);
                    return End();
                }
                else if (TurnPlayer.InUnderground)
                {
                    TurnPlayer.FromCatacomb(ClickSector);
                    return End();
                }
                else if (PlayerSelectWizardSector)
                {
                    Game.ClearSelectedSectors();
                    PlayerSelectWizardSector = false;
                    ClickSector.AddPlayer(new Wizard(TurnPlayer, ClickSector));
                    return Moved();
                }
                else
                {
                    Game.ClearSelectedSectors();
                    var path = PathCreate(ClickSector, out int StepCount);
                    bool HaveWizard = TurnPlayer.HaveCard<WizardCard>(out int _);
                    Sounds["Move"].Play(100);
                    bool EndTurn = await TurnPlayer.MoveToSector(path.Select(S => S.TblLocation).ToArray());
                    Sounds["Move"].Stop();
                    PlayerDiceCheck -= StepCount;
                    if (!PlayerUseCard)
                    {
                        if (path.Any(S => S.Type == Sector.TypeSector.Ford))
                            PlayerDiceCheck++;
                        if (path.Any(S => S.Type == Sector.TypeSector.GoldBridge))
                        {
                            TurnPlayer.UseCard<FiveGoldCard>();
                            PlayerDiceCheck++;
                        }
                    }
                    else
                    {
                        if (path.Any(S => S.Type == Sector.TypeSector.Water ||  S.Type == Sector.TypeSector.Ford ||
                                          S.Type == Sector.TypeSector.Bridge || S.Type == Sector.TypeSector.GoldBridge ||
                                          S.Type == Sector.TypeSector.DifficultStandart))
                            TurnPlayer.UseCard<DragonKhiragir>();
                    }
                    if (HaveWizard != TurnPlayer.HaveCard<WizardCard>(out int _))
                    {
                        PlayerSelectWizardSector = true;
                        var NextSectorsMap = Game.PlayerCanGo(TurnPlayer, 1, 1, false);
                        if (TurnPlayer.IsBot)
                        {
                            Point[] variants = TurnPlayer.Bot.MoveEndVariants(NextSectorsMap, 1);
                            Point answer = TurnPlayer.Bot.GetPlaceAnswer(Bot.PlaceTurnCase.WizardSpawnPlace, variants);
                            return await SectorSelected(Game[answer]);
                        }
                        else
                        {
                            Game.SectorsSelect(TurnPlayer, NextSectorsMap);
                            return this;
                        }
                    }
                    else if (TurnPlayer.Aboard)
                        return GoOnWater();
                    else if (PlayerDiceCheck > 0)
                    {
                        if (EndTurn)
                            return End();
                        else
                            return await GoToMove(PlayerDiceCheck, PlayerUseCard);
                    }
                    else
                        return Moved();
                }
            }
            private Turn Moved()
            {
                var AttackSectors = TurnPlayer.Attack();
                if (AttackSectors.Length > 0)
                {
                    string[] Texts = Localization[LocalizationKeys.PlayerCanAttack].Split("{}".ToCharArray());
                    Texts[1] = string.Join(", ", AttackSectors.SelectMany(S => S.GetPlayers()).Where(P => TurnPlayer.IsOpponent(P)).Select(P => P.Name).Distinct());
                    if (TurnPlayer.Team == Team.Wizard)
                    {
                        foreach (Player player in AttackSectors[0].GetPlayers().Where(P => TurnPlayer.IsOpponent(P) && !P.InUnderground))
                        {
                            player.Damage();
                            player.Damage();
                        }
                        TurnPlayer.Damage();
                    }
                    else if (TurnPlayer.SendMessage(Texts.Aggregate((S0, S1) => S0 + S1),
                                                    Localization[LocalizationKeys.PlayerAttack],
                                                    Bot.QuestionTurnCase.AttackQuestion,
                                                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        PlayerIsAttack = true;
                        var PlayerLocation = TurnPlayer.TblLocation;
                        var AttackList = new List<Point>();
                        if (AttackSectors.Where(S => S.TblX < PlayerLocation.X).Any())
                            AttackList.Add(PlayerLocation + new Size(-1, 0));
                        if (AttackSectors.Where(S => S.TblY > PlayerLocation.Y).Any())
                            AttackList.Add(PlayerLocation + new Size(0, 1));
                        if (AttackSectors.Where(S => S.TblX > PlayerLocation.X).Any())
                            AttackList.Add(PlayerLocation + new Size(1, 0));
                        if (AttackSectors.Where(S => S.TblY < PlayerLocation.Y).Any())
                            AttackList.Add(PlayerLocation + new Size(0, -1));
                        if (TurnPlayer.IsBot)
                        {
                            Point answer = TurnPlayer.Bot.GetPlaceAnswer(Bot.PlaceTurnCase.AttackDirection, AttackList.ToArray());
                            return SectorSelected(Game[answer]).Result;
                        }
                        else
                        {
                            Game.SectorsSelect(AttackList.ToArray());
                            return this;
                        }
                    }
                }
                return End();
            }
            private Turn LoseTurn()
            {
                TurnPlayer.LoseTurn = false;
                string[] Texts = Localization[LocalizationKeys.PlayerLoseTurn].Split("{}".ToCharArray());
                Texts[1] = TurnPlayer?.Name;
                MessageBox.Show(Texts.Aggregate((S0, S1) => S0 + S1));
                Game.ClearSelectedSectors();
                return End();
            }
            public Turn End()
            {
                PlayerPathMap = null;
                Game.ClearSelectedSectors();
                PlayerDiceCheck = 0;
                PlayerFullDiceCheck = 0;
                PlayerUseCard = false;
                PlayerIsAttack = false;
                AttackDirection = -1;
                if (!Game.Start)
                    return null;
                else if (TurnPlayer.HaveCard<WizardCard>(out _) && !WizardTurned)
                {
                    if (TurnPlayer.Wizard.LoseTurn)
                        return new Turn(TurnPlayer.Wizard, DF).LoseTurn();
                    else
                        return new Turn(TurnPlayer.Wizard, DF);
                }
                else
                {
                    WizardTurned = false;
                    switch (TurnPlayer.Team)
                    {
                        case Team.Light:
                        {
                            int i = Game.PlayersOf(Team.Light).ToList().IndexOf(TurnPlayer);
                            Player player = Game.PlayersOf(Team.Dark)[i];
                            if (player.IsDead)
                                return new Turn(player, DF).End();
                            else if (player.LoseTurn)
                                return new Turn(player, DF).LoseTurn();
                            else
                                return new Turn(player, DF);
                        }
                        case Team.Dark:
                        {
                            int i = Game.PlayersOf(Team.Dark).ToList().IndexOf(TurnPlayer) + 1;
                            if (Game.PlayersOf(Team.Light).Length == i)
                                i = 0;
                            Player player = Game.PlayersOf(Team.Light)[i];
                            if (player.IsDead)
                                return new Turn(player, DF).End();
                            else if (player.LoseTurn)
                                return new Turn(player, DF).LoseTurn();
                            else
                                return new Turn(player, DF);
                        }
                        case Team.Wizard:
                        {
                            TurnPlayer = (TurnPlayer as Wizard).Owner;
                            WizardTurned = true;
                            return End();
                        }
                    }
                }
                return null;
            }
        }
    }
}
