using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueRuby
{
    public partial class StartForm : Form
    {
        private Deckmode[] Deckmodes;

        private Localization Localization => MainForm.GetMainForm.Settings.Localization;

        public StartForm()
        {
            InitializeComponent();
            Text = Localization[LocalizationKeys.MainForm].Split('_')[2];
            string[] Texts = Localization[LocalizationKeys.StartForm].Split('_');
            GameModeBox.Text = Texts[0];
            AddPlayerButton.Text = Texts[1];
            DeckCompilationBox.Text = Texts[2];
            LiveCompilationBox.Text = Texts[3];
            SaveParamsButton.Text = Texts[4];
            DeleteButton.Text = Texts[5];
            LoadButton.Text = Texts[6];
            StartButton.Text = Texts[7];
            Texts = Localization[LocalizationKeys.Gamemodes].Split('_');
            PlayerVSPlayerMode.Text = Texts[0];
            TeamVSTeamMode.Text = Texts[1];
            Texts = Localization[LocalizationKeys.Cards].Split('_');
            FiveGold_Check.Text = Texts[0];
            TenGold_Check.Text = Texts[1];
            Spell_Check.Text = Texts[2];
            Shield_Check.Text = Texts[3];
            Dragon_Check.Text = Texts[4];
            Wizard_Check.Text = Texts[5];
            Texts = Localization[LocalizationKeys.LiveCompilation].Split('_');
            StartLive_Label.Text = Texts[0];
            DeckLive_Check.Text = Texts[1];

            static int MaxWidth(Control.ControlCollection controls)
            {
                int Max = 0;
                foreach (int width in from Control C in controls
                                      select C.Width)
                    if (Max < width)
                        Max = width;
                return Max;
            }

            int gamemodeMax = MaxWidth(GameModeBox.Controls) + 14;
            GameModeBox.Size = new Size(gamemodeMax, GameModeBox.Height);
            AddPlayerButton.Size = new Size(gamemodeMax, AddPlayerButton.Height);
            DeckModesBox.Size = new Size(gamemodeMax, DeckModesBox.Height);
            PlayersPanel.Location = new Point(GameModeBox.Right + 6, PlayersPanel.Top);
            PlayersPanel.Size = new Size(563 - PlayersPanel.Left, PlayersPanel.Height);

            int cardsMax = MaxWidth(DeckCompilationBox.Controls) + 63;
            DeckCompilationBox.Size = new Size(cardsMax, DeckCompilationBox.Height);
            LiveCompilationBox.Size = new Size(cardsMax, LiveCompilationBox.Height);
            SaveParamsButton.Size = new Size(cardsMax, SaveParamsButton.Height);
            DeleteButton.Size = new Size(cardsMax, DeleteButton.Height);

            Deckmodes = Deckmode.Load();
            DeckModesBox.Items.AddRange(Deckmodes);
            PlayersPanel.ControlAdded += (sender, e) => CheckPlayersNames();
            PlayersPanel.ControlRemoved += (sender, e) => CheckPlayersNames();
            CheckPlayersCount();
        }

        private PlayerPanel GetPlayerPanel(int index)
        {
            PlayerPanel panel = new PlayerPanel()
            {
                Location = new Point(3, 3 + 33 * index + PlayersPanel.AutoScrollPosition.Y),
            };
            panel.NameChanged += (sender, e) => CheckPlayersNames();
            panel.TeamChanged += (sender, e) => CheckTeamCounts();
            panel.PlayerDeleted += (sender, e) =>
            {
                ReShowPlayers();
                CheckPlayersCount();
            };
            return panel;
        }
        private void ReShowPlayers()
        {
            for (int i = 0; i < PlayersPanel.Controls.Count; i++)
                PlayersPanel.Controls[i].Location = new Point(3, 3 + 33 * i + PlayersPanel.AutoScrollPosition.Y);
        }

        private bool checkCount = false;
        private bool checkName = false;
        private bool checkTeams = false;
        private bool CheckCount
        {
            get => checkCount;
            set
            {
                checkCount = value;
                CheckStart();
            }
        }
        private bool CheckName
        {
            get => checkName;
            set
            {
                checkName = value;
                CheckStart();
            }
        }
        private bool CheckTeams
        {
            get => checkTeams;
            set
            {
                checkTeams = value;
                CheckStart();
            }
        }
        private void CheckPlayersCount()
        {
            void Error(string Message)
            {
                CheckCount = false;
                CountError.SetIconAlignment(StartButton, ErrorIconAlignment.MiddleLeft);
                CountError.SetError(StartButton, Message);
            }
            CheckCount = true;
            CountError.Clear();
            int playersCount = PlayersPanel.Controls.Count;
            if ((PlayerVSPlayerMode.Checked && playersCount >= 2) ||
                (TeamVSTeamMode.Checked && playersCount == 8))
                AddPlayerButton.Enabled = false;
            else
                AddPlayerButton.Enabled = true;
            if (PlayerVSPlayerMode.Checked && playersCount > 2)
            {
                MessageBox.Show(Localization[LocalizationKeys.PlayerVSPlayerError]);
                for (int i = 2; i < playersCount; i++)
                    PlayersPanel.Controls[2].Dispose();
            }
            if (playersCount == 0)
                Error(Localization[LocalizationKeys.NullPlayersError]);
            else if (playersCount % 2 != 0)
                Error(Localization[LocalizationKeys.UnevenCountError]);
            CheckTeamCounts();
        }
        private void CheckPlayersNames()
        {
            CheckName = true;
            foreach (Control P in PlayersPanel.Controls)
                (P as PlayerPanel).NameErrorClear();
            var playersNames = from Control C in PlayersPanel.Controls
                               let P = C as PlayerPanel
                               group P by P.PlayerName;
            foreach (var group in playersNames)
            {
                if (group.Key == "")
                    foreach (var panel in group)
                    {
                        panel.NameError(Localization[LocalizationKeys.EmptyNameError]);
                        CheckName = false;
                    }
                else if (group.Count() > 1)
                    foreach (var panel in group)
                    {
                        string[] Texts = Localization[LocalizationKeys.RepeatNamesError].Split("{}".ToCharArray());
                        Texts[1] = group.Key;
                        Texts[3] = group.Count().ToString();
                        panel.NameError(Texts.Aggregate((S0, S1) => S0 + S1));
                        CheckName = false;
                    }
            }
        }
        private void CheckTeamCounts()
        {
            CheckTeams = true;
            foreach (Control P in PlayersPanel.Controls)
                (P as PlayerPanel).TeamErrorClear();
            int playersCount = PlayersPanel.Controls.Count / 2;
            var playersTeam = from Control C in PlayersPanel.Controls
                              let P = C as PlayerPanel
                              group P by P.PlayerTeam;
            foreach (var group in playersTeam)
            {
                if (group.Count() > playersCount)
                {
                    string team = group.Key switch
                    {
                        Team.Light => LocalizationKeys.LightTeamCountError,
                        Team.Dark => LocalizationKeys.DarkTeamCountError,
                        _ => null
                    };
                    if (team == null)
                        continue;
                    else
                        foreach (var panel in group)
                        {
                            string[] Texts = Localization[team].Split("{}".ToCharArray());
                            Texts[1] = (group.Count() - playersCount).ToString();
                            panel.TeamError(Texts.Aggregate((S0, S1) => S0 + S1));
                            CheckTeams = false;
                        }
                }
            }
        }
        private void CheckStart()
            => StartButton.Enabled = CheckCount && CheckName && CheckTeams;

        private void Gamemode_Changed(object sender, EventArgs e)
            => CheckPlayersCount();
        private void AddPlayer_Click(object sender, EventArgs e)
        {
            PlayersPanel.Controls.Add(GetPlayerPanel(PlayersPanel.Controls.Count));
            if (PlayersPanel.Controls.Count % 2 != 0)
                PlayersPanel.Controls.Add(GetPlayerPanel(PlayersPanel.Controls.Count));
            CheckPlayersCount();
        }

        private bool DeckModeChanging = false;
        private void DeckMode_Changed(object sender, EventArgs e)
        {
            bool DeckmodeSelected = DeckModesBox.SelectedIndex != -1;
            SaveParamsButton.Enabled = !DeckmodeSelected;
            DeleteButton.Enabled = DeckmodeSelected && DeckModesBox.SelectedItem.ToString() != "Standart";
            if (DeckmodeSelected)
            {
                DeckModeChanging = true;
                var deckmode = DeckModesBox.SelectedItem as Deckmode;
                int number = deckmode.Deck[typeof(FiveGoldCard)];
                FiveGold_Numeric.Value = number;
                FiveGold_Check.Checked = number > 0;
                number = deckmode.Deck[typeof(TenGoldCard)];
                TenGold_Numeric.Value = number;
                TenGold_Check.Checked = number > 0;
                number = deckmode.Deck[typeof(SpellMagicFire)];
                Spell_Numeric.Value = number;
                Spell_Check.Checked = number > 0;
                number = deckmode.Deck[typeof(ShieldTaygarol)];
                Shield_Numeric.Value = number;
                Shield_Check.Checked = number > 0;
                number = deckmode.Deck[typeof(DragonKhiragir)];
                Dragon_Numeric.Value = number;
                Dragon_Check.Checked = number > 0;
                number = deckmode.Deck[typeof(WizardCard)];
                Wizard_Numeric.Value = number;
                Wizard_Check.Checked = number > 0;
                StartLive_Numeric.Value = deckmode.LiveCount;
                number = deckmode.DeckLiveCount;
                DeckLive_Numeric.Value = number;
                DeckLive_Check.Checked = number != 0;
                DeckModeChanging = false;
            }
        }
        private void DeckParams_Changed(object sender, EventArgs e)
        {
            if (!DeckModeChanging)
            {
                DeckModesBox.SelectedIndex = -1;
                var sendername = (sender as Control).Name.Split('_');
                (string CardType, string ControlType) = (sendername[0], sendername[1]);
                if (ControlType == "Check")
                    (DeckCompilationBox.Controls[CardType + "_Numeric"] as NumericUpDown).Value = (sender as CheckBox).Checked ? 1 : 0;
                else if (ControlType == "Numeric")
                    (DeckCompilationBox.Controls[CardType + "_Check"] as CheckBox).Checked = (sender as NumericUpDown).Value != 0;
            }
        }
        private void LiveParams_Changed(object sender, EventArgs e)
        {
            if (!DeckModeChanging)
            {
                DeckModesBox.SelectedIndex = -1;
                var sendername = (sender as Control).Name.Split('_');
                (string ParamType, string ControlType) = (sendername[0], sendername[1]);
                if (ControlType == "Check")
                    (LiveCompilationBox.Controls[ParamType + "_Numeric"] as NumericUpDown).Value = (sender as CheckBox).Checked ? 1 : 0;
                else if (ControlType == "Numeric" && LiveCompilationBox.Controls[ParamType + "_Check"] != null)
                {
                    DeckModeChanging = true;
                    (LiveCompilationBox.Controls[ParamType + "_Check"] as CheckBox).Checked = (sender as NumericUpDown).Value != 0;
                    DeckModeChanging = false;
                }
            }
        }

        private void SaveParamsButton_Click(object sender, EventArgs e)
        {
            TextDialog F = new TextDialog();
            if (F.ShowDialog() == DialogResult.OK)
            {
                var deckmode = new Deckmode(F.OutName,
                                            CreateDeck(),
                                            (int)StartLive_Numeric.Value,
                                            (int)DeckLive_Numeric.Value);
                deckmode.Save();
                DeckModesBox.Items.Add(deckmode);
            }
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if ((DeckModesBox.SelectedItem as Deckmode).Delete())
            {
                DeckModesBox.Items.Remove(DeckModesBox.SelectedItem);
                DeckModesBox.SelectedIndex = -1;
                MessageBox.Show("Delete complete");
            }
            else
                MessageBox.Show("Delete no complete");
        }
        private Dictionary<Type, int> CreateDeck() => new Dictionary<Type, int>
        {
            { typeof(FiveGoldCard), FiveGold_Check.Checked ? (int)FiveGold_Numeric.Value : 0 },
            { typeof(TenGoldCard), TenGold_Check.Checked ? (int)TenGold_Numeric.Value : 0 },
            { typeof(SpellMagicFire), Spell_Check.Checked ? (int)Spell_Numeric.Value : 0 },
            { typeof(ShieldTaygarol), Shield_Check.Checked ? (int)Shield_Numeric.Value : 0 },
            { typeof(DragonKhiragir), Dragon_Check.Checked ? (int)Dragon_Numeric.Value : 0 },
            { typeof(WizardCard), Wizard_Check.Checked ? (int)Wizard_Numeric.Value : 0 }
        };

        private void StartButton_Click(object sender, EventArgs e)
        {
            int liveCount = (int)StartLive_Numeric.Value;
            int playersCount = PlayersPanel.Controls.Count / 2;
            var players = PlayersPanel.Controls.Cast<PlayerPanel>().Select(P => P.GetPlayer(liveCount)).ToList();
            int darkCount = players.Where(P => P.Team == Team.Dark).Count();
            int lightCount = players.Where(P => P.Team == Team.Light).Count();
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Team == Team.Fantom)
                {
                    Team getTeam;
                    if (lightCount == playersCount)
                        getTeam = Team.Dark;
                    else if (darkCount == playersCount)
                        getTeam = Team.Light;
                    else
                        getTeam = (Team)(Expansion.Rnd.Next() % 2 + 1);
                    players[i] = new Player(players[i].Name, getTeam, liveCount, players[i].IsBot);
                    darkCount = players.Where(P => P.Team == Team.Dark).Count();
                    lightCount = players.Where(P => P.Team == Team.Light).Count();
                }
            }
            if (PlayerVSPlayerMode.Checked)
            {
                var AllPlayers = new List<Player>();
                for (int k = 0; k < 2; k++)
                    for (int i = 1; i <= 4; i++)
                        AllPlayers.Add(new Player(players[k].Name + "@" + i,
                                                  players[k].Team,
                                                  liveCount,
                                                  players[k].IsBot));
                players = AllPlayers;
            }

            int DeckLiveCount = (int)DeckLive_Numeric.Value;
            StartGame(new GameTable.GameData(players.ToArray(),
                                             DeckModesBox.SelectedIndex == -1 ?
                                             new Deckmode("", CreateDeck(),
                                                              (int)StartLive_Numeric.Value,
                                                              DeckLiveCount > 0 && DeckLiveCount < players.Sum(P => P.Live) ?
                                                              players.Sum(P => P.Live) : DeckLiveCount)
                                             : DeckModesBox.SelectedItem as Deckmode),
                      !checkBox1.Checked, this);
        }
        private void LoadButton_Click(object sender, EventArgs e)
        {
            SaveLoadDialog F = new SaveLoadDialog(SaveLoadDialog.DialogMode.Load);
            if (F.ShowDialog() == DialogResult.OK)
                StartGame(F.Data, F.Data.TurnPlayer, this);
        }

        public static void QuickStart(Settings.QuickStartParams startParams)
        {
            Player[] players = startParams.QuickStartNames.SelectMany(N =>
            {
                var intEnum = Enumerable.Range(0, N.Value.Length).ToList().Shuffle();
                return Enumerable.Range(0, startParams.PlayersCount / 2).Select(I => new Player(N.Value[intEnum[I]],
                                                                                                N.Key,
                                                                                                startParams.Deckmode.LiveCount));
            }).ToArray();
            StartGame(new GameTable.GameData(players, startParams.Deckmode), startParams.Lottery);
        }
        private static void StartGame(GameTable.GameData data, object startState, StartForm form)
        {
            GameForm F = new GameForm(data);
            F.Loading(startState, form);
        }
        private static void StartGame(GameTable.GameData data, bool lottery)
            => StartGame(data, lottery, null);
        public static void StartGame(GameTable.GameData data, string turnPlayer)
            => StartGame(data, turnPlayer, null);
    }

    public class Deckmode
    {
        public string Name { get; }
        public Dictionary<Type, int> Deck { get; }
        public List<Card> CreateDeck()
            => Deck.SelectMany(C => Enumerable.Repeat(Activator.CreateInstance(C.Key) as Card, C.Value)).ToList();
        private Dictionary<string, int> Livemode { get; }
        public int LiveCount => Livemode["LiveCount"];
        public int DeckLiveCount => Livemode["DeckLiveCount"];

        public Deckmode(string name, Dictionary<Type, int> deck, int lives, int maxLives)
        {
            Name = name;
            if (deck.Count != 6 && !deck.All(C => Activator.CreateInstance(C.Key) is Card))
                throw new Exception();
            Deck = deck;
            Livemode = new Dictionary<string, int>()
            {
                { "LiveCount", lives },
                { "DeckLiveCount", maxLives }
            };
        }
        public static string ModesDatPath => MainForm.ExecutablePath + "\\Deckmodes.dat";
        private static List<string[]> ReadModesData()
        {
            List<string[]> AllParams = new List<string[]>();
            using (var SR = new StreamReader(ModesDatPath))
            {
                List<string> Params = new List<string>();
                while (!SR.EndOfStream)
                {
                    string line = HexConvert.OutHex(SR.ReadLine());
                    if (line == "@")
                    {
                        AllParams.Add(Params.ToArray());
                        Params.Clear();
                    }
                    else
                        Params.Add(line);
                }
                if (Params.Count > 0)
                {
                    AllParams.Add(Params.ToArray());
                    Params.Clear();
                }
            }
            return AllParams;
        }
        public static string[] ParamsNames()
        {
            if (File.Exists(ModesDatPath))
                return ReadModesData().Select(Ps => Ps[0]).ToArray();
            else
                return new string[0];
        }

        public void Save()
        {
            var Params = Name +
                         Deck.Select(V => $"\n{V.Key.Name}@{V.Value}").Aggregate((S0, S1) => S0 + S1) +
                         "\nLiveCount@" + LiveCount +
                         "\nDeckLiveCount@" + DeckLiveCount;
            if (File.Exists(ModesDatPath))
                File.AppendAllLines(ModesDatPath, Params.Split('\n').Prepend("@").Select(S => HexConvert.InHex(S)));
            else
                File.WriteAllLines(ModesDatPath, Params.Split('\n').Select(S => HexConvert.InHex(S)));
        }
        public static Deckmode[] Load()
        {
            if (File.Exists(ModesDatPath))
            {
                return ReadModesData().Select(Ps =>
                {
                    string Name = Ps[0];
                    string[] Deck = new string[6];
                    string[] LiveDeck = new string[2];
                    Array.Copy(Ps, 1, Deck, 0, 6);
                    Array.Copy(Ps, 7, LiveDeck, 0, 2);
                    Dictionary<string, int> livesMode = LiveDeck.Select(S => S.Split('@'))
                                                                .ToDictionary(L => L[0], L => int.Parse(L[1]));
                    return new Deckmode(Name,
                                        Deck.Select(S => S.Split('@')).ToDictionary(C => Card.Cards[C[0]], C => int.Parse(C[1])),
                                        livesMode["LiveCount"], livesMode["DeckLiveCount"]);
                }).ToArray();
            }
            else
                return new Deckmode[0];
        }
        public bool Delete()
        {
            List<string[]> AllParams = ReadModesData();
            int i = AllParams.RemoveAll(Ps => Ps[0] == Name);
            if (i == 0)
                return false;
            else
            {
                string content = AllParams.Select(Ps => string.Join("\n", Ps)).Aggregate((Ps0, Ps1) => $"{Ps0}\n@\n{Ps1}");
                File.WriteAllLines(ModesDatPath, content.Split('\n').Select(S => HexConvert.InHex(S)));
                return true;
            }
        }

        public override string ToString()
            => Name;
    }
}