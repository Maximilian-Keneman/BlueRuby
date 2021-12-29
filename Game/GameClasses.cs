using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueRuby
{
    public class Sector
    {
        public static Sector LocationSaveSector(Point Location)
            => new Sector(new Size(30, 30), TypeSector.Empty, 4, 0, Point.Empty, Location);

        public bool[] CanGo = new bool[4] { false, false, true, false };
        public Image BackGround
        {
            get
            {
                Image img;
                if (CanSelected)
                {
                    int opacity = Selected ? 150 : 100;
                    img = Images.SetSquare(Color.FromArgb(opacity, Color.Yellow), Size);
                }
                else if (Attacked)
                    img = Images.SetSquare(Color.FromArgb(100, Color.Orange), Size);
                else
                    img = new Bitmap(Convert.ToInt32(Size.Width), Convert.ToInt32(Size.Height));
                using (Graphics g = Graphics.FromImage(img))
                {
                    if (Players[0] != null)
                        g.DrawImage(Players[0].Texture, 2.5f, 2.5f);
                    if (Players[1] != null)
                        g.DrawImage(Players[1].Texture, 17.5f, 2.5f);
                    if (Players[2] != null)
                        g.DrawImage(Players[2].Texture, 2.5f, 17.5f);
                    if (Players[3] != null)
                        g.DrawImage(Players[3].Texture, 17.5f, 17.5f);
                }
                return img;
            }
        }
        private bool _CanSelected = false;
        private bool _Selected = false;
        private bool _Attacked = false;
        public bool CanSelected
        {
            get => _CanSelected;
            set
            {
                _CanSelected = value;
                OnTextureChanged();
            }
        }
        public bool Selected
        {
            get => _Selected;
            set
            {
                _Selected = value;
                OnTextureChanged();
            }
        }
        public bool Attacked
        {
            get => _Attacked;
            set
            {
                _Attacked = value;
                OnTextureChanged();
            }
        }
        public event EventHandler TextureChanged;
        private void OnTextureChanged() => TextureChanged?.Invoke(this, EventArgs.Empty);

        public enum TypeSector
        {
            Empty,
            StartLight,
            StartDark,
            FinishLight,
            FinishDark,
            Standart,
            DifficultStandart,
            Ship,
            Water,
            Ford,
            Bridge,
            GoldBridge,
            MagicTower,
            Fortress,
            Catacomb,
            Underground
        }
        public TypeSector Type;

        #region Position
        public Point ImgLocation { get; }
        public Size Size { get; }
        public Rectangle GetBounds => new Rectangle(ImgLocation, Size);
        public Point TblLocation { get; }
        public int TblX => TblLocation.X;
        public int TblY => TblLocation.Y;
        #endregion

        #region Players
        private Player[] Players = new Player[4];
        public bool AddPlayer(Player player)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i] == null)
                {
                    Players[i] = player;
                    player.LocationSector = this;
                    OnTextureChanged();
                    return true;
                }
            }
            return false;
        }
        public void LeavePlayer(Player player)
        {
            for (int i = 0; i < Players.Length; i++)
                if (Players[i] == player)
                    Players[i] = null;
            OnTextureChanged();
        }
        public Player[] GetPlayers() => Players.Where(P => P != null).ToArray();
        #endregion

        public Sector(Size Size, bool[] CanGo)
        {
            Type = TypeSector.Standart;
            this.Size = Size;
            if (CanGo.Length != 4)
                throw new NumberException("CanGo.Length", 4);
            for (int i = 0; i < CanGo.Length; i++)
                this.CanGo[i] = CanGo[i];
        }
        public Sector(Size Size, TypeSector type, int Wall, int Dir, Point imgLocation, Point tblLocation)
        {
            Type = type;
            this.Size = Size;
            ImgLocation = imgLocation;
            TblLocation = tblLocation;
            CanGo = Wall switch
            {
                0 => new bool[] { true, true, true, true },
                1 => Dir switch
                {
                    0 => new bool[] { true, true, false, true },
                    1 => new bool[] { true, true, true, false },
                    2 => new bool[] { false, true, true, true },
                    3 => new bool[] { true, false, true, true },
                    _ => throw new NumberException("Dir", 0, 3),
                },
                2 => Dir switch
                {
                    0 => new bool[] { true, true, false, false },
                    1 => new bool[] { false, true, true, false },
                    2 => new bool[] { false, false, true, true },
                    3 => new bool[] { true, false, false, true },
                    4 => new bool[] { false, true, false, true },
                    5 => new bool[] { true, false, true, false },
                    _ => throw new NumberException("Dir", 0, 5),
                },
                3 => Dir switch
                {
                    0 => new bool[] { true, false, false, false },
                    1 => new bool[] { false, true, false, false },
                    2 => new bool[] { false, false, true, false },
                    3 => new bool[] { false, false, false, true },
                    _ => throw new NumberException("Dir", 0, 3),
                },
                4 => new bool[] { false, false, false, false },
                _ => throw new NumberException("Wall", 0, 4),
            };
        }
    }

    public partial class GameTable : IDisposable
    {
        public Sector[,] Sectors { get; }
        public Sector this[int X, int Y] => Sectors[X, Y];
        public Sector this[Point coordinates] => Sectors[coordinates.X, coordinates.Y];
        public Size SectorsSize => new Size(Sectors.GetLength(0), Sectors.GetLength(1));
        public readonly Size Size;
        public Point Location;

        public ObservableCollection<Card> Deck;
        #region LiveDeck
        protected int MaxLiveCount;
        public bool IsInfiniteLiveDeck => MaxLiveCount < 0;
        public bool IsNullLiveDeck => MaxLiveCount == 0;
        protected int _liveDeckCount;
        public int LiveDeckCount
        {
            get => _liveDeckCount;
            set
            {
                _liveDeckCount = value;
                LiveDeckChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler LiveDeckChanged;
        private void LiveCalculate() => LiveDeckCount = MaxLiveCount - Players.Sum(P => P.IsDead ? 0 : P.Live);
        #endregion

        #region Players
        private Player[] Players
        {
            get
            {
                List<Player> ret = new List<Player>();
                foreach (Sector sector in Sectors)
                    ret.AddRange(sector.GetPlayers());
                return ret.ToArray();
            }
        }
        public void SetPlayers(List<Player> players)
        {
            foreach (Player player in players)
            {
                player.OwnerGame = this;
                player.LiveChanged += (sender, e) => LiveCalculate();
                if (player.LocationSector == null)
                    switch (player.Team)
                    {
                        case Team.Light:
                            Sectors[24, 1].AddPlayer(player);
                            break;
                        case Team.Dark:
                            Sectors[24, 20].AddPlayer(player);
                            break;
                        default:
                            break;
                    }
                else
                    this[player.TblLocation].AddPlayer(player);
            }
            LiveCalculate();
            if (PlayersOf(Team.Light).Any(P => P.IsBot))
                LightBot = new TeamBot(this, Team.Light);
            if (PlayersOf(Team.Dark).Any(P => P.IsBot))
                DarkBot = new TeamBot(this, Team.Dark);
        }
        public Player[] AllPlayers => Players;
        public Player FindPlayer(string name) => Players.Single(P => P.Name == name);
        public Player[] PlayersOf(Team team) => Players.Where(P => P.Team == team).OrderBy(P => P.Name).ToArray();
        public Player[] LosePlayers => Players.Where(P => P.LoseTurn).ToArray();
        #endregion
        public bool HaveBot => Players.Any(P => P.IsBot);
        public TeamBot LightBot { get; private set; }
        public TeamBot DarkBot { get; private set; }
        public WorldParts.BotMaps Maps { get; private set; }
        public void LoadMaps(WorldParts.BotMaps maps) => Maps = maps;

        public bool Start { get; protected set; } = false;

        public GameTable(Rectangle TableSize, (int WD, Point Location, Sector.TypeSector Type)[,] Level, List<Card> Deck, int LiveDeckCount)
        {
            Size S = new Size(Level.GetLength(0), Level.GetLength(1));
            Sectors = new Sector[S.Width, S.Height];
            Size = TableSize.Size;
            Location = TableSize.Location;
            for (int i = 0; i < S.Width; i++)
            {
                for (int j = 0; j < S.Height; j++)
                {
                    int W = Level[i, j].WD / 10;
                    int D = Level[i, j].WD % 10;
                    int sectorSizeX, sectorSizeY;
                    sectorSizeX = j < S.Height - 1 ? Level[i, j + 1].Location.X - Level[i, j].Location.X : TableSize.Right - Level[i, j].Location.X;
                    sectorSizeY = i < S.Width - 1 ? Level[i + 1, j].Location.Y - Level[i, j].Location.Y : TableSize.Bottom - Level[i, j].Location.Y;
                    Sectors[i, j] = new Sector(new Size(sectorSizeX, sectorSizeY), Level[i,j].Type, W, D, Level[i, j].Location, new Point(i, j));
                    Sectors[i, j].TextureChanged += HaveChanges;
                }
            }
            this.Deck = new ObservableCollection<Card>(Deck);
            MaxLiveCount = this.LiveDeckCount = LiveDeckCount;
            ChangeTimer = new System.Threading.Timer(Redraw, null, -1, 10);
        }
        public GameTable(GameTable game)
        {
            Sectors = game.Sectors;
            for (int i = 0; i < SectorsSize.Width; i++)
                for (int j = 0; j < SectorsSize.Height; j++)
                    Sectors[i, j].TextureChanged += HaveChanges;
            Size = game.Size;
            Location = game.Location;
            Deck = game.Deck;
            MaxLiveCount = LiveDeckCount = game.LiveDeckCount;
            ChangeTimer = new System.Threading.Timer(Redraw, null, -1, 10);
        }
        public bool IsDebug => this is DebugGame;
        public void StartGame(bool NewGame, bool lottery)
        {
            Start = true;
            if (NewGame && !lottery)
            {
                int Lucky = Expansion.Rnd.Next(Players.Length);
                var pls = Players;
                for (int i = 0; i < pls.Length; i++)
                    pls[i].TakeBlueRuby(new BlueRubyCard(i == Lucky));
            }
            ChangeTimer.Change(0, 10);
            LightBot?.GiveStrategies();
            DarkBot?.GiveStrategies();
        }

        public class GameData
        {
            public string TurnPlayer { get; }
            public Player[] Players { get; }
            public List<Card> Deck { get; }
            public int LiveCount { get; }

            public GameData(Player[] players, Deckmode deckmode)
            {
                Players = players;
                Deck = deckmode.CreateDeck();
                LiveCount = deckmode.DeckLiveCount;
            }
            public GameData(Player[] players, List<Card> deck, int liveCount)
            {
                Players = players;
                Deck = deck;
                LiveCount = liveCount;
            }
            public GameData(Player[] players, List<Card> deck, int liveCount, string turnPlayer) : this(players, deck, liveCount)
            {
                TurnPlayer = turnPlayer;
            }

            private enum DataFill
            {
                Players,
                Deck
            }
            public GameData(string data)
            {
                List<Player> players = new List<Player>();
                Deck = new List<Card>();
                DataFill mode = DataFill.Players;
                var danie = data.Split('\n');
                foreach (string D in danie)
                {
                    if (D == "")
                        continue;
                    else if (D == "Players")
                        mode = DataFill.Players;
                    else if (D == "Deck")
                        mode = DataFill.Deck;
                    else if (D.StartsWith("TurnPlayer"))
                        TurnPlayer = D.Split('@')[1];
                    else if (D.StartsWith("LiveCount"))
                        LiveCount = int.Parse(D.Replace("LiveCount", ""));
                    else if (mode == DataFill.Players)
                        players.Add(new Player(new Player.PlayerData(D)));
                    else if (mode == DataFill.Deck)
                        Deck.Add(Activator.CreateInstance(Card.Cards[D]) as Card);
                    else
                        throw new Exception();
                }
                Players = players.Select(P => P.Team == Team.Wizard ? new Wizard(players.Single(Pl => Pl.Name == P.Name.Split('@')[0]), P.LocationSector) : P).ToArray();
            }

            public override string ToString() =>
                $"Players\n{Players.Select(P => P.GetPlayerData().ToString()).Aggregate((S0, S1) => $"{S0}\n{S1}")}\n" +
                $"Deck\n{(Deck.Count > 0 ? Deck.Select(C => C.ToString()).Aggregate((S0, S1) => $"{S0}\n{S1}") : "")}\n" +
                $"LiveCount{LiveCount}\n" +
                $"TurnPlayer@{TurnPlayer}";
        }
        public GameData GetGameData(string turnPlayer) => new GameData(Players, Deck.ToList(), LiveDeckCount, turnPlayer);

        #region Draw Game
        protected System.Threading.Timer ChangeTimer;
        private bool IsChanged = false;
        private void HaveChanges(object sender, EventArgs e) => IsChanged = true;
        private void Redraw(object slate)
        {
            if (IsChanged)
            {
                ImageChanged?.Invoke(this, EventArgs.Empty);
                IsChanged = false;
            }
        }
        public Image Show()
        {
            Image Game = new Bitmap(Convert.ToInt32(Size.Width), Convert.ToInt32(Size.Height));
            using Graphics g = Graphics.FromImage(Game);
            for (int i = 0; i < SectorsSize.Width; i++)
                for (int j = 0; j < SectorsSize.Height; j++)
                    g.DrawImage(Sectors[i, j].BackGround, Sectors[i, j].ImgLocation);
            if (PlayerMoving.Player != null)
                g.DrawImage(PlayerMoving.Player.Texture, PlayerMoving.Position);
            return Game;
        }
        public event EventHandler ImageChanged;
        #endregion
        #region Animations
        private (Player Player, Point Position) PlayerMoving = (null, new Point());

        public async Task AttackAnimation(Sector[] sectors)
        {
            ClearSelectedSectors();
            foreach (var S in sectors)
            {
                S.Attacked = true;
                await Task.Delay(500);
            }
        }
        public async Task MoveAnimation(Player player, Sector[] sectors)
        {
            var path = sectors.Select(S => S.ImgLocation + new Size(S.Size.Width / 2 - 3, S.Size.Height / 2 - 3)).ToArray();
            for (int i = 0; i < path.Length - 1; i++)
            {
                var position = path[i];
                PlayerMoving = (player, position);
                var finPoint = path[i + 1];
                var dirPath = (finPoint - position.ToSize()).ToSize();
                var dir = new Size(Math.Abs(dirPath.Width) < Math.Abs(dirPath.Height) ? 0 : ((dirPath.Width * 10 + 1) / Math.Abs(dirPath.Width * 10 + 1)),
                                   Math.Abs(dirPath.Height) < Math.Abs(dirPath.Width) ? 0 : ((dirPath.Height * 10 + 1) / Math.Abs(dirPath.Height * 10 + 1)));
                while ((dirPath.Width * dir.Width + dirPath.Height * dir.Height) /
                       (Math.Sqrt(dirPath.Width * dirPath.Width + dirPath.Height * dirPath.Height) *
                        Math.Sqrt(dir.Width * dir.Width + dir.Height * dir.Height)) > 0)
                {
                    position += dir;
                    dirPath = (finPoint - position.ToSize()).ToSize();
                    PlayerMoving = (player, position);
                    IsChanged = true;
                    await Task.Delay(10);
                }
            }
            PlayerMoving = (null, new Point());
        }
        #endregion
        #region Clear Sectors States
        public void ClearAttackedSectors()
        {
            foreach (Sector sector in Sectors)
                sector.Attacked = false;
        }
        public void DeselectedSectors()
        {
            foreach (Sector sector in Sectors)
                sector.Selected = false;
        }
        public void ClearSelectedSectors()
        {
            foreach (Sector sector in Sectors)
            {
                sector.CanSelected = false;
                sector.Selected = false;
            }
        }
        #endregion

        public Sector SectorFromPoint(Point point)
        {
            if (point.X <= Size.Width && point.Y <= Size.Height &&
                point.X >= 0 && point.Y >= 0)
            {
                int N = point.Y / 30,
                    M = point.X / 30;
                if (M < SectorsSize.Height && N < SectorsSize.Width &&
                    M >= 0 && N >= 0)
                {
                    var rect = Sectors[N, M].GetBounds;
                    if (!rect.Contains(point))
                    {
                        if (point.Y > rect.Bottom)
                            N++;
                        if (point.Y < rect.Top)
                            N--;
                        if (point.X > rect.Right)
                            M++;
                        if (point.X < rect.Left)
                            M--;
                    }
                    if (M < SectorsSize.Height && N <= SectorsSize.Width &&
                        M >= 0 && N >= 0 && Sectors[N, M].GetBounds.Contains(point))
                        return Sectors[N, M];
                }
            }
            return null;
        }

        public class GameMap
        {
            private int[,] map;
            public int this[int X, int Y] => map[X, Y];
            public int this[Point location] => map[location.X, location.Y];
            public Size Size => new Size(map.GetLength(0), map.GetLength(1));
            public Point StartPoint { get; private set; }

            public static GameMap ClearMap(Size size)
            {
                int[,] newMap = new int[size.Width, size.Height];
                for (int x = 0; x < size.Width; x++)
                    for (int y = 0; y < size.Height; y++)
                        newMap[x, y] = -1;
                return new GameMap(newMap);
            }
            public GameMap(int[,] map)
            { this.map = map; }

            public GameMap(Sector[,] Sectors, Point start, int stepCount, bool CanDifficultMove, bool Ford, bool UseDragon, bool HaveGold)
            {
                map = CreateMap(Sectors, start, stepCount, CanDifficultMove, Ford, UseDragon, HaveGold, new List<Point>());
                StartPoint = start;
            }
            public GameMap(Sector[,] Sectors, Point start, bool UseDragon, bool HaveGold)
            {
                map = CreateMap(Sectors, start, Sectors.Length, true, false, UseDragon, HaveGold, new List<Point>());
                StartPoint = start;
            }
            private int[,] CreateMap(Sector[,] Sectors, Point start, int StepCount, bool CanDifficultMove, bool Ford, bool UseDragon, bool UseGold, List<Point> NoGo)
            {
                Size size = new Size(Sectors.GetLength(0), Sectors.GetLength(1));
                int[,] CheckTable = new int[size.Width, size.Height];
                for (int n = 0; n < size.Width; n++)
                    for (int m = 0; m < size.Height; m++)
                        CheckTable[n, m] = -1;
                int c = 0;
                CheckTable[start.X, start.Y] = 0;
                List<Point> checkedList = new List<Point>();
                List<Point> checkList = new List<Point>() { start };
                List<Point> nextList = new List<Point>();
                while (c < StepCount)
                {
                    foreach (Point sector in checkList)
                    {
                        var nextSectors = new Point[]
                        {
                            new Point(sector.X - 1, sector.Y),
                            new Point(sector.X, sector.Y + 1),
                            new Point(sector.X + 1, sector.Y),
                            new Point(sector.X, sector.Y - 1)
                        }.Where(P => (P.X >= 0 && P.X < size.Width && P.Y >= 0 && P.Y < size.Height) && !checkedList.Contains(P));
                        foreach (Point next in nextSectors)
                        {
                            (int g1, int g2) = (sector, next).GoDir();
                            if (g1 == -1)
                                continue;
                            if ((Sectors[sector.X, sector.Y].CanGo[g1] && Sectors[next.X, next.Y].CanGo[g2]
                                || (UseDragon && (Sectors[sector.X, sector.Y].Type == Sector.TypeSector.Water ||
                                                  Sectors[next.X, next.Y].Type == Sector.TypeSector.Water)))
                                && (CheckTable[next.X, next.Y] > CheckTable[sector.X, sector.Y] || CheckTable[next.X, next.Y] == -1)
                                && Sectors[next.X, next.Y].GetPlayers().Length < 4)
                            {
                                CheckTable[next.X, next.Y] = CheckTable[sector.X, sector.Y] + 1;
                                if (Sectors[next.X, next.Y].Type == Sector.TypeSector.DifficultStandart)
                                {
                                    if (!CanDifficultMove && !UseDragon)
                                        CheckTable[next.X, next.Y] = -1;
                                }
                                else if (Sectors[next.X, next.Y].Type == Sector.TypeSector.Ford)
                                {
                                    if (NoGo.Contains(next) || Ford)
                                        CheckTable[next.X, next.Y] = -1;
                                    else if (!UseDragon)
                                    {
                                        int dir = sector.X - next.X;
                                        Point p = new Point(next.X - dir, next.Y);
                                        int CheckNumber = CheckTable[sector.X, sector.Y] + 2;
                                        if (CheckTable[p.X, p.Y] > CheckNumber || CheckTable[p.X, p.Y] == -1)
                                        {
                                            int[,] AddCheckTable = CreateMap(Sectors, p, StepCount - CheckNumber + 1, CanDifficultMove, true, false, UseGold, NoGo.Append(next).ToList());
                                            for (int x = 0; x < AddCheckTable.GetLongLength(0); x++)
                                                for (int y = 0; y < AddCheckTable.GetLongLength(1); y++)
                                                    if (AddCheckTable[x, y] > -1 && CheckTable[x, y] == -1)
                                                        CheckTable[x, y] = AddCheckTable[x, y] + CheckNumber;
                                        }
                                    }
                                }
                                else if (Sectors[next.X, next.Y].Type == Sector.TypeSector.GoldBridge)
                                {
                                    if (NoGo.Contains(next) || !UseGold)
                                        CheckTable[next.X, next.Y] = -1;
                                    else if (!UseDragon)
                                    {
                                        int dir = sector.X - next.X;
                                        Point p = new Point(next.X - dir, next.Y);
                                        int CheckNumber = CheckTable[sector.X, sector.Y] + 2;

                                        if (CheckTable[p.X, p.Y] > CheckNumber || CheckTable[p.X, p.Y] == -1)
                                        {
                                            int[,] AddCheckTable = CreateMap(Sectors, p, StepCount - CheckNumber + 1, CanDifficultMove, false, false, false, NoGo.Append(next).ToList());
                                            for (int x = 0; x < AddCheckTable.GetLongLength(0); x++)
                                                for (int y = 0; y < AddCheckTable.GetLongLength(1); y++)
                                                    if (AddCheckTable[x, y] > -1 && CheckTable[x, y] == -1)
                                                        CheckTable[x, y] = AddCheckTable[x, y] + CheckNumber;
                                        }
                                    }
                                }
                                else if (Sectors[next.X, next.Y].Type == Sector.TypeSector.Bridge)
                                {
                                    if (!UseDragon && !NoGo.Contains(next))
                                    {
                                        Point p;
                                        if (Sectors[next.X, next.Y - 3].Type == Sector.TypeSector.Bridge)
                                            p = new Point(next.X, next.Y - 3);
                                        else if (Sectors[next.X, next.Y + 3].Type == Sector.TypeSector.Bridge)
                                            p = new Point(next.X, next.Y + 3);
                                        else if (Sectors[next.X - 1, next.Y + 3].Type == Sector.TypeSector.Bridge)
                                            p = new Point(next.X - 1, next.Y + 3);
                                        else if (Sectors[next.X + 1, next.Y - 3].Type == Sector.TypeSector.Bridge)
                                            p = new Point(next.X + 1, next.Y - 3);
                                        else
                                            throw new Exception("Not found other bridge point");

                                        int CheckNumber = CheckTable[next.X, next.Y] + 1;

                                        if (CheckTable[p.X, p.Y] > CheckNumber || CheckTable[p.X, p.Y] == -1)
                                        {
                                            int[,] AddCheckTable = CreateMap(Sectors, p, StepCount - CheckNumber, CanDifficultMove, false, false, UseGold, NoGo.Append(next).ToList());
                                            for (int x = 0; x < AddCheckTable.GetLongLength(0); x++)
                                                for (int y = 0; y < AddCheckTable.GetLongLength(1); y++)
                                                    if (AddCheckTable[x, y] > -1 && CheckTable[x, y] == -1)
                                                        CheckTable[x, y] = AddCheckTable[x, y] + CheckNumber;
                                        }
                                    }
                                }
                            }
                        }
                        nextList.AddRange(nextSectors.Where(P => CheckTable[P.X, P.Y] != -1));
                    }
                    checkedList.AddRange(checkList);
                    checkList.Clear();
                    checkList.AddRange(nextList.Distinct());
                    nextList.Clear();
                    c++;
                }
                return CheckTable;
            }

            public static implicit operator int[,](GameMap map) => map.map;
        }
        public GameMap PlayerCanGo(Player player, int DiceCheck, int FullDiceCheck, bool UseDragon)
        {
            return new GameMap(Sectors, player.TblLocation, DiceCheck,
                               FullDiceCheck % 2 != 0, player.LoseTurn, UseDragon, player.HaveCard<FiveGoldCard>(out _));
        }
        public void SectorsSelect(Player player, GameMap map)
        {
            for (int n = 0; n < SectorsSize.Width; n++)
                for (int m = 0; m < SectorsSize.Height; m++)
                {
                    Point loc = new Point(n, m);
                    if (map[loc] > 0 && this[loc].Type != Sector.TypeSector.Water &&
                        !this[loc].GetPlayers().Any(P => P.Team == Team.Wizard && player.IsOpponent(P)))
                        this[loc].CanSelected = true;
                }
        }
        public void SectorsSelect(Point[] sectors)
        {
            foreach (Point P in sectors)
                this[P].CanSelected = true;
        }
        public bool NoMoveCheck(GameMap map)
        {
            for (int n = 0; n < SectorsSize.Width; n++)
                for (int m = 0; m < SectorsSize.Height; m++)
                    if (map[n, m] == 1)
                        return false;
            return true;
        }

        public Point[] Catacombs()
        {
            List<Point> ps = new List<Point>();
            for (int n = 0; n < SectorsSize.Width; n++)
                for (int m = 0; m < SectorsSize.Height; m++)
                    if (Sectors[n, m].Type == Sector.TypeSector.Catacomb && Sectors[n, m].GetPlayers().Length < 4)
                        ps.Add(new Point(n, m));
            return ps.ToArray();
        }

        public event EventHandler GameEnded;
        public bool WinBlueRuby(Player player)
        {
            if (player.BlueRuby.Where(B => B.IsReal).Any())
            {
                Win(player.Team);
                return true;
            }
            else
                return false;
        }
        public void Win(Team team)
        {
            Start = false;
            GameEnded?.Invoke(team, EventArgs.Empty);
        }


        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ImageChanged = new EventHandler((o, e) => { });
                    GameEnded = new EventHandler((o, e) => { });
                    // TODO: освободить управляемое состояние (управляемые объекты)
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
                // TODO: установить значение NULL для больших полей
                disposedValue = true;
            }
        }

        // // TODO: переопределить метод завершения, только если "Dispose(bool disposing)" содержит код для освобождения неуправляемых ресурсов
        // ~GameTable()
        // {
        //     // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}