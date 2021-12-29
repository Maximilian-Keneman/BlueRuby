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
    public enum Team
    {
        Fantom,
        Light,
        Dark,
        Wizard
    }
    public class Player
    {
        //public static Player FantomPlayer(Point location)
        //    => new Player("", Team.Fantom, 0, false) { LocationSector = Sector.LocationSaveSector(location) };

        public string Name { get; }
        public Team Team { get; }
        public bool IsOnlinePlayer { get; }
        public Bot Bot;
        public bool IsBot => Bot != null;

        #region Live/Energy
        private int _live;
        public int Live
        {
            get => _live;
            set
            {
                _live = value;
                LiveChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler LiveChanged;
        #endregion

        #region Deck/Player's Cards
        private List<Card> Deck;
        public Image[] GetDeck() => Deck.Select(C => C.Texture).ToArray();
        public bool HaveCard<T>(out int count) where T : Card
        {
            if (Team == Team.Wizard)
                count = 0;
            else
                count = Deck.Count(C => C is T);
            return count > 0;
        }
        public bool UseCard<T>() where T : Card
        {
            int i = Deck.FindIndex(C => C is T);
            if (i == -1)
                return false;
            else
            {
                OwnerGame.Deck.Add(Deck[i]);
                Deck.RemoveAt(i);
                OnDeckChanged();
                return true;
            }
        }
        public void RetrunAllCards()
        {
            foreach (Card card in Deck)
                OwnerGame.Deck.Add(card);
            Deck.Clear();
            OnDeckChanged();
        }
        private void OnDeckChanged() => DeckChanged?.Invoke(this, EventArgs.Empty);
        public event EventHandler DeckChanged;
        public int Gold => Deck?.Where(C => C is GoldCard).Cast<GoldCard>().Sum(C => C.Value) ?? 0;
        public bool UseGold(int count)
        {
            if (Gold < count)
                return false;
            int Purchase = 0;
            if (count % 2 != 0)
            {
                if (!HaveCard<FiveGoldCard>(out _))
                    return false;
                else
                {
                    UseCard<FiveGoldCard>();
                    Purchase += 5;
                }
            }
            while (Purchase < count && HaveCard<TenGoldCard>(out _))
            {
                UseCard<TenGoldCard>();
                Purchase += 10;
            }
            while (Purchase < count && HaveCard<FiveGoldCard>(out _))
            {
                UseCard<FiveGoldCard>();
                Purchase += 5;
            }
            return true;
        }
        public Wizard Wizard = null;
        #endregion

        #region Blue Ruby
        public List<BlueRubyCard> BlueRuby { get; private set; }
        public void TakeBlueRuby(BlueRubyCard ruby)
        {
            BlueRuby.Add(ruby);
            OnBlueRubyListChanged();
        }
        public event EventHandler BlueRubyListChanged;
        private void OnBlueRubyListChanged()
        {
            BlueRubyListChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        private Localization Localization => MainForm.GetMainForm.Settings.Localization;
        private Dictionary<Sector.TypeSector, string> Interactives
        {
            get
            {
                string[] Texts = Localization[LocalizationKeys.Interactives].Split('_');
                return new Dictionary<Sector.TypeSector, string>()
                {
                    { Sector.TypeSector.MagicTower, Texts[0] },
                    { Sector.TypeSector.Fortress, Texts[1] },
                    { Sector.TypeSector.Catacomb, Texts[2] },
                    { Sector.TypeSector.Ship, Texts[3] }
                };
            }
        }
        public GameTable OwnerGame;
        public Sector LocationSector;
        public Point TblLocation => LocationSector.TblLocation;
        public Image Texture
        {
            get
            {
                Image playersTexture = Properties.Resources.playerstextures;
                if (IsDead)
                {
                    if (BlueRuby.Count > 0)
                        return Images.GetFragment(playersTexture, new Size(64, 64), new Rectangle(0, 128, 64, 64), new Size(10, 10));//Images.SetRound(Color.DarkBlue, 5);
                    else
                        return Images.GetFragment(playersTexture, new Size(64, 64), new Rectangle(64, 128, 64, 64), new Size(10, 10));//Images.SetRound(Color.Gray, 5);
                }
                else
                {
                    Image WizardTexture()
                    {
                        if (this is Wizard)
                            return (this as Wizard).Owner.Team switch
                            {
                                Team.Light => Images.GetFragment(playersTexture, new Size(64, 64), new Rectangle(0, 64, 64, 64), new Size(10, 10)),
                                Team.Dark => Images.GetFragment(playersTexture, new Size(64, 64), new Rectangle(64, 64, 64, 64), new Size(10, 10)),
                                _ => Images.SetRound(Color.GreenYellow, 5)
                            };
                        else
                            return Images.SetRound(Color.GreenYellow, 5);
                    }
                    return Team switch
                    {
                        Team.Light => Images.GetFragment(playersTexture, new Size(64, 64), new Rectangle(0, 0, 64, 64), new Size(10, 10)),//Images.SetRound(Color.Blue, 5),//
                        Team.Dark => Images.GetFragment(playersTexture, new Size(64, 64), new Rectangle(64, 0, 64, 64), new Size(10, 10)),//Images.SetRound(Color.Red, 5),
                        Team.Wizard => WizardTexture(),//Images.SetRound(Color.GreenYellow, 5),
                        _ => Images.SetRound(Color.White, 5)
                    };
                }
            }
        }

        public Player(string name, Team team, int liveCount, bool isBot = false)
        {
            Name = name;
            Team = team;
            Live = liveCount;
            if (Team != Team.Wizard)
            {
                Deck = new List<Card>();
                BlueRuby = new List<BlueRubyCard>();
            }
            if (isBot)
                Bot = new Bot(this);
        }
        public Player(PlayerData data)
        {
            Name = data.Name;
            Team = (Team)Enum.Parse(typeof(Team), data.Team);
            if (data.Bot != null)
            {
                Bot = new Bot(this);
                Bot.SetStrategy(data.Bot.Value.Strategy);
                int min = data.Bot.Value.Parametrs / 1000,
                    max = data.Bot.Value.Parametrs / 100 % 10,
                    bad = data.Bot.Value.Parametrs / 10 % 100,
                    good = data.Bot.Value.Parametrs % 1000;
                Bot.SetParametrs(min, max, bad, good);
            }
            _live = data.Live;
            Deck = data.Deck.SelectMany(C => Enumerable.Repeat(Activator.CreateInstance(C.Key) as Card, C.Value)).ToList();
            if (data.BlueRuby.Count > 0)
            {
                BlueRuby = Enumerable.Repeat(new BlueRubyCard(false), data.BlueRuby.Count - 1).ToList();
                for (int i = 0; i < data.BlueRuby.OpenCount; i++)
                    BlueRuby[i].Open();
                BlueRuby.Add(new BlueRubyCard(data.BlueRuby.HaveReal));
            }
            else
                BlueRuby = new List<BlueRubyCard>();
            LocationSector = Sector.LocationSaveSector(data.Location);
            LoseTurn = data.LoseTurn;
            LastFortress = data.LastFortress;
            LastMagicTower = data.LastMagicTower;
        }

        public class PlayerData
        {
            public string Name { get; }
            public string Team { get; }
            public (Bot.Strategy Strategy, int Parametrs)? Bot { get; }
            public int Live { get; }
            public Dictionary<Type, int> Deck { get; }
            public (int Count, int OpenCount, bool HaveReal) BlueRuby { get; }
            public Point Location { get; }
            public bool LoseTurn { get; }
            public Point LastMagicTower { get; }
            public Point LastFortress { get; }

            public PlayerData(string name, Team team, Bot bot, int live, List<Card> deck, List<BlueRubyCard> blueRuby,
                              Point location, bool loseTurn, Point lastMagicTower, Point lastFortress)
            {
                Name = name;
                Team = team.ToString();
                if (bot == null)
                    Bot = null;
                else
                    Bot = (bot.MyStrategy, bot.GetParametrs());
                Live = live;
                Deck = deck == null ? new Dictionary<Type, int>() : deck.GroupBy(C => C.GetType()).ToDictionary(G => G.Key, G => G.Count());
                BlueRuby = blueRuby == null ? (0, 0, false) : (blueRuby.Count, blueRuby.Count(R => R.IsOpen), blueRuby.Any(R => R.IsReal));
                Location = location;
                LoseTurn = loseTurn;
                LastMagicTower = lastMagicTower;
                LastFortress = lastFortress;
            }
            public PlayerData(string dat)
            {
                var data = dat.Split('@');
                if (data.Length > 10)
                    data = new string[] { data[0] + '@' + data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10] };
                Name = data[0];
                Team = data[1];
                if (data[2] == "")
                {
                    string strategy = new string(data[2].Take(data[2].Length - 4).ToArray());
                    string parametrs = new string(data[2].Skip(data[2].Length - 4).ToArray());
                    Bot = (Strategy: (Bot.Strategy)Enum.Parse(typeof(Bot.Strategy), strategy),
                           Parametrs: int.Parse(parametrs));
                }
                Live = int.Parse(data[3]);
                Deck = new Dictionary<Type, int>();
                if (data[4] != "")
                {
                    var cards = data[4].Split(',');
                    for (int i = 0; i < cards.Length - 1; i += 2)
                        Deck.Add(Card.Cards[cards[i]], int.Parse(cards[i + 1]));
                }
                int[] BlueRubyCipher = data[5].Select(C => int.Parse(C.ToString())).ToArray();
                BlueRuby = (BlueRubyCipher[0], BlueRubyCipher[1], BlueRubyCipher[2] == 1);
                var point = data[6].Split(',').Select(C => int.Parse(C)).ToArray();
                Location = new Point(point[0], point[1]);
                LoseTurn = data[7] == "1";
                point = data[8].Split(',').Select(C => int.Parse(C)).ToArray();
                LastMagicTower = new Point(point[0], point[1]);
                point = data[9].Split(',').Select(C => int.Parse(C)).ToArray();
                LastFortress = new Point(point[0], point[1]);
            }

            public override string ToString() =>
                   $"{Name}@{Team}@{(Bot.HasValue ? Bot.Value.Strategy.ToString() + Bot.Value.Parametrs : "")}@{Live}@" +
                   $"{(Deck.Count > 0 ? Deck.Select(C => $"{C.Key.Name},{C.Value}").Aggregate((S0, S1) => $"{S0},{S1}") : "")}@" +
                   $"{BlueRuby.Count}{BlueRuby.OpenCount}{(BlueRuby.HaveReal ? 1 : 0)}@" +
                   $"{Location.X},{Location.Y}@{(LoseTurn ? 1 : 0)}@" +
                   $"{LastMagicTower.X},{LastMagicTower.Y}@{LastFortress.X},{LastFortress.Y}";
        }
        public PlayerData GetPlayerData() =>
            new PlayerData(Name, Team, Bot, Live, Deck, BlueRuby, TblLocation, LoseTurn, LastMagicTower, LastFortress);

        public bool LoseTurn = false;
        public async Task<bool> MoveToSector(Point[] path)
        {
            LocationSector.LeavePlayer(this);
            await OwnerGame.MoveAnimation(this, path.Select(P => OwnerGame[P]).ToArray());
            OwnerGame[path.Last()].AddPlayer(this);
            bool EndTurn = false;
            if (path.Select(P => OwnerGame[P]).Where(S => S.Type == Sector.TypeSector.Ford).Any())
                LoseTurn = true;
            if (Team != Team.Wizard)
                switch (LocationSector.Type)
                {
                    case Sector.TypeSector.FinishLight:
                        if (Team == Team.Light)
                        {
                            foreach (BlueRubyCard blue in BlueRuby)
                                blue.Open();
                            OnBlueRubyListChanged();
                            EndTurn = OwnerGame.WinBlueRuby(this);
                        }
                        break;
                    case Sector.TypeSector.FinishDark:
                        if (Team == Team.Dark)
                        {
                            foreach (BlueRubyCard blue in BlueRuby)
                                blue.Open();
                            OnBlueRubyListChanged();
                            EndTurn = OwnerGame.WinBlueRuby(this);
                        }
                        break;
                    case Sector.TypeSector.Ship:
                        EndTurn = GoOnBoard();
                        break;
                    case Sector.TypeSector.MagicTower:
                        if (!OwnerGame.IsNullLiveDeck)
                            EndTurn = InMagicTower();
                        break;
                    case Sector.TypeSector.Fortress:
                        EndTurn = InFortress();
                        break;
                    case Sector.TypeSector.Catacomb:
                        EndTurn = SendMessage(Localization[LocalizationKeys.CatacombQuestion],
                                              Interactives[Sector.TypeSector.Catacomb],
                                              Bot.QuestionTurnCase.CatacombQuestion,
                                              MessageBoxButtons.YesNo) == DialogResult.Yes;
                        break;
                }
            else if (LocationSector.Type == Sector.TypeSector.Catacomb)
                EndTurn = SendMessage(Localization[LocalizationKeys.CatacombQuestion],
                                      Interactives[Sector.TypeSector.Catacomb],
                                      Bot.QuestionTurnCase.CatacombQuestion,
                                      MessageBoxButtons.YesNo) == DialogResult.Yes;
            var DeadPlayers = LocationSector.GetPlayers().Where(P => P.IsDead);
            if (DeadPlayers.Any())
                foreach (Player player in DeadPlayers)
                {
                    foreach (var ruby in player.BlueRuby)
                        TakeBlueRuby(ruby);
                    player.BlueRuby.Clear();
                    player.OnBlueRubyListChanged();
                    OnBlueRubyListChanged();
                }
            return EndTurn;
        }

        public DialogResult SendMessage(string msg, string title, Bot.QuestionTurnCase? botCase, MessageBoxButtons mboxButtons)
        {
            var dictionary = new Dictionary<MessageBoxButtons, MessageBoxIcon>
            {
                { MessageBoxButtons.YesNo, MessageBoxIcon.Question },
                { MessageBoxButtons.OK, MessageBoxIcon.Exclamation }
            };
            if (IsBot)
            {
                if (botCase.HasValue)
                    return Bot.GetQuestionAnswer(botCase.Value);
                else
                    return DialogResult.None;
            }
            else
                return MessageBox.Show(msg, title, mboxButtons, dictionary[mboxButtons]);
        }

        #region Fight
        public bool IsOpponent(Player opponent)
        {
            if (opponent.Team == Team.Wizard)
                return Team.Opponent() == (opponent as Wizard).Owner.Team;
            else if (Team == Team.Wizard)
                return opponent.Team == (this as Wizard).Owner.Team.Opponent();
            else
                return Team.Opponent() == opponent.Team;
        }
        public Sector[] Attack()
        {
            if (Team == Team.Wizard)
                return LocationSector.GetPlayers().Where(P => IsOpponent(P) && !P.IsDead && !P.InUnderground).Select(P => P.LocationSector).ToArray();
            Point P = TblLocation;
            List<Sector> AttackSectors = new List<Sector>();
            for (int i = 1; i <= 6; i++)
            {
                List<Size> NextSectors = new List<Size>();
                if (P.X < OwnerGame.SectorsSize.Width - i)
                    NextSectors.Add(new Size(i, 0));
                if (P.Y < OwnerGame.SectorsSize.Height - i)
                    NextSectors.Add(new Size(0, i));
                if (P.X > i - 1)
                    NextSectors.Add(new Size(-i, 0));
                if (P.Y > i - 1)
                    NextSectors.Add(new Size(0, -i));
                AttackSectors.AddRange(NextSectors.Select(N => OwnerGame[P + N])
                                                  .Where(S => S.GetPlayers().Any(P => IsOpponent(P) && !P.IsDead && !P.InUnderground)));
            }
            return AttackSectors.ToArray();
        }
        public void Damage()
        {
            if (Team == Team.Wizard || !UseCard<ShieldTaygarol>())
                Live--;
            if (Live < 0)
                Dead();
        }
        public bool IsDead => Live < 0;
        protected virtual void Dead()
        {
            RetrunAllCards();
            if (OwnerGame.PlayersOf(Team).All(P => P.IsDead))
                OwnerGame.Win(Team.Opponent());
        }
        #endregion

        #region Magic Tower/Fortress
        public Point LastMagicTower { get; private set; } = new Point(-1, -1);
        public Point LastFortress { get; private set; } = new Point(-1, -1);
        public bool InMagicTower()
        {
            if (TblLocation != LastMagicTower)
            {
                if (OwnerGame.LiveDeckCount > 0)
                {
                    if (SendMessage(Localization[LocalizationKeys.MagicTowerQuestion],
                                    Interactives[Sector.TypeSector.MagicTower],
                                    Bot.QuestionTurnCase.MagicTowerQuestion,
                                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (Gold < 10)
                        {
                            SendMessage(Localization[LocalizationKeys.NoMoney],
                                        Interactives[Sector.TypeSector.MagicTower],
                                        null, MessageBoxButtons.OK);
                        }
                        else if (!UseGold(10))
                        {
                            SendMessage(Localization[LocalizationKeys.NoSmallMoney],
                                        Interactives[Sector.TypeSector.MagicTower],
                                        null, MessageBoxButtons.OK);
                        }
                        else
                        {
                            Live++;
                            LastMagicTower = TblLocation;
                            return true;
                        }
                    }
                }
                else
                    SendMessage(Localization[LocalizationKeys.DeckLose],
                                Interactives[Sector.TypeSector.MagicTower],
                                null, MessageBoxButtons.OK);
            }
            else
                SendMessage(Localization[LocalizationKeys.MagicTowerNoDoubleEnter],
                            Interactives[Sector.TypeSector.MagicTower],
                            null, MessageBoxButtons.OK);
            return false;
        }
        public bool InFortress()
        {
            if (TblLocation != LastFortress)
            {
                if (OwnerGame.Deck.Count > 0)
                {
                    if (SendMessage(Localization[LocalizationKeys.FortressQuestion],
                                    Interactives[Sector.TypeSector.Fortress],
                                    Bot.QuestionTurnCase.FortressQuestion,
                                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        bool TakeCard(int trycount)
                        {
                            Deck.Add(OwnerGame.Deck[0]);
                            OwnerGame.Deck.RemoveAt(0);
                            if (HaveCard<WizardCard>(out int count) && count > 1)
                            {
                                trycount++;
                                UseCard<WizardCard>();
                                if (trycount <= OwnerGame.Deck.Count)
                                    return TakeCard(trycount);
                                else
                                    return false;
                            }
                            return true;
                        }
                        LastFortress = TblLocation;
                        if (TakeCard(0))
                        {
                            OnDeckChanged();
                            return true;
                        }
                        else
                            SendMessage(Localization[LocalizationKeys.NoDoubleWizard],
                                        Interactives[Sector.TypeSector.Fortress],
                                        null, MessageBoxButtons.OK);
                    }
                }
                else
                    SendMessage(Localization[LocalizationKeys.DeckLose],
                                Interactives[Sector.TypeSector.Fortress],
                                null, MessageBoxButtons.OK);
            }
            else
                SendMessage(Localization[LocalizationKeys.FortressNoDoubleEnter],
                            Interactives[Sector.TypeSector.Fortress],
                            null, MessageBoxButtons.OK);
            return false;
        }
        #endregion

        #region Ship
        public bool Aboard = false;
        public bool GoOnBoard()
        {
            if (SendMessage(Localization[LocalizationKeys.ShipQuestion],
                            Interactives[Sector.TypeSector.Ship],
                            Bot.QuestionTurnCase.ShipQuestion,
                            MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Gold < 15)
                {
                    SendMessage(Localization[LocalizationKeys.NoMoney],
                                Interactives[Sector.TypeSector.Ship],
                                null, MessageBoxButtons.OK);
                }
                else if (!UseGold(15))
                {
                    SendMessage(Localization[LocalizationKeys.NoSmallMoney],
                                Interactives[Sector.TypeSector.Ship],
                                null, MessageBoxButtons.OK);
                }
                else
                {
                    Aboard = true;
                    return true;
                }
            }
            return false;
        }
        public void FormShip(Sector sector)
        {
            LocationSector.LeavePlayer(this);
            sector.AddPlayer(this);
            Aboard = false;
        }
        #endregion

        #region Catacomb
        public bool AtCatacomb => LocationSector.Type == Sector.TypeSector.Catacomb;
        public bool InUnderground => LocationSector.Type == Sector.TypeSector.Underground;
        public void InCatacomb()
        {
            LocationSector.LeavePlayer(this);
            OwnerGame[WorldParts.UndergroundPosition[Team]].AddPlayer(this);
        }
        public void FromCatacomb(Sector sector)
        {
            LocationSector.LeavePlayer(this);
            sector.AddPlayer(this);
        }
        #endregion

        public override string ToString() => $"Name = {Name}, Team = {Team}";
    }
    public class Wizard : Player
    {
        public Player Owner;

        public Wizard(Player player, Sector sector) : base(player.Name + "@wizard", Team.Wizard, 0)
        {
            if (player.IsBot)
            {
                Bot = new Bot(this);
                Bot.SetStrategy(Bot.Strategy.Warior);
            }
            OwnerGame = player.OwnerGame;
            Owner = player;
            player.Wizard = this;
            LocationSector = sector;
        }

        protected override void Dead()
        {
            Owner.UseCard<WizardCard>();
            Owner.Wizard = null;
            LocationSector.LeavePlayer(this);
        }
    }
}