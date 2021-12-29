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
    public partial class GameForm : Form
    {
        private Localization Localization => MainForm.GetMainForm.Settings.Localization;
        public GameTable Game;
        public GameTable.Turn Turn;
        private DiceForm DF;
        private TeamForm DarkF;
        private TeamForm LightF;

        private PictureBox GameBox;

        public Dictionary<string, Sound> Sounds = new Dictionary<string, Sound>()
        {
            { "Move", new Sound(MainForm.ExecutablePath + "\\sounds\\grass.wav") },
            { "Attack", new Sound(MainForm.ExecutablePath + "\\sounds\\MagicFire.wav") },
        };
        public static GameForm GetGameForm => Application.OpenForms.OfType<GameForm>().Single();

        public GameForm(GameTable.GameData data)
        {
            InitializeComponent();
            Text = Localization[LocalizationKeys.MainForm].Split('_')[0];
            Game = new GameTable(new Rectangle(5, 53, 664, 817), WorldParts.Level, data.Deck.Shuffle(), data.LiveCount);
            Game.SetPlayers(data.Players.ToList());
            GameBox = new PictureBox()
            {
                Image = Game.Show(),
                Location = Game.Location,
                Size = Game.Size,
                BackColor = Color.Transparent
            };
            GameBox.MouseMove += GameBox_MouseMove;
            GameBox.MouseClick += GameBox_MouseClick;
            GameBox.MouseWheel += GameBox_MouseWheel;
            GameBackgroundBox.Controls.Add(GameBox);
            Game.ImageChanged += (sender, e) =>
            {
                try
                {
                    GameBox.Image = Game.Show();
                }
                catch
                { }
            };
            Game.GameEnded += (sender, e) =>
            {
                string[] Texts = Localization[LocalizationKeys.TeamWin].Split("{}".ToCharArray());
                Texts[1] = (sender as Team?).ToString();
                MessageBox.Show(Texts.Aggregate((S0, S1) => S0 + S1));
                Turn = Turn.End();
            };
        }

        public void Loading(object startState, StartForm form)
        {
            if (Game.HaveBot)
            {
                LoadingForm F = new LoadingForm(WorldParts.BotMaps.InteractiviesCount(Game), startState, form);
                F.Show(this);
                F.Start(Game);
            }
            else
                LoadingComplete(startState, form);
        }
        public void DebugLoading()
        {
            Game = new DebugGame(Game);
            Game.ImageChanged += (sender, e) =>
            {
                try
                {
                    DebugForm DF = OwnedForms.OfType<DebugForm>().Single();
                    GameBox.Image = (Game as DebugGame).Show(DF.Mode, DF.Mode switch
                    {
                        DebugForm.DebugMode.TblLoaction => null,
                        DebugForm.DebugMode.BotMaps => DF.BotMapsMode,
                        DebugForm.DebugMode.Empty => null,
                        DebugForm.DebugMode.Ship => null,
                    });
                }
                catch
                { }
            };
            Game.GameEnded += (sender, e) =>
            {
                string[] Texts = Localization[LocalizationKeys.TeamWin].Split("{}".ToCharArray());
                Texts[1] = (sender as Team?).ToString();
                MessageBox.Show(Texts.Aggregate((S0, S1) => S0 + S1));
                Turn = Turn.End();
            };
            LoadingForm F = new LoadingForm(WorldParts.BotMaps.InteractiviesCount(Game), null, null);
            F.Show(this);
            F.Start(Game);
        }
        public void LoadingComplete(WorldParts.BotMaps maps, object startState, StartForm form)
        {
            Game.LoadMaps(maps);
            LoadingComplete(startState, form);
        }
        private async void LoadingComplete(object startState, StartForm form)
        {
            if (form != null)
            {
                form.DialogResult = DialogResult.OK;
                form.Close();
                await Task.Delay(10);
            }
            if (startState is null)
            {
                Show();
                DebugForm F = new DebugForm();
                F.Show(this);
                (Game as DebugGame).StartGame();
            }
            else
            {
                FormsShow();
                if (startState is bool lottery)
                    Start(lottery);
                else if (startState is string turnPlayer)
                    Start(turnPlayer);
            }
        }
        private void FormsShow()
        {
            Show();
            DF = new DiceForm();
            DarkF = new TeamForm(Team.Dark, this);
            LightF = new TeamForm(Team.Light, this);
            GameMenuForm MenuF = new GameMenuForm();
            DesktopLocation = new PointF(DarkF.Width * 1.125f, Screen.PrimaryScreen.Bounds.Height - (Height + DF.Height)).ToPoint();
            Rectangle bounds = DesktopBounds;
            DF.Show(this);
            DF.SetDesktopLocation((int)(bounds.X + (bounds.Width - DF.Width) * 0.5f), (int)(bounds.Bottom - DF.Height * 0.5f));
            DarkF.Show(this);
            DarkF.SetDesktopLocation(bounds.X - DarkF.Width - 10, bounds.Y);
            LightF.Show(this);
            LightF.SetDesktopLocation(bounds.Right + 10, bounds.Y);
            MenuF.Show(this);
            MenuF.SetDesktopLocation(LightF.DesktopBounds.Left, LightF.DesktopBounds.Bottom);
            MenuF.DeckLoad();
            ChatForm CF = new ChatForm(Game.AllPlayers[0], Game.AllPlayers);
            CF.Show();
        }
        private void Start(bool lottery)
        {
            if (lottery)
            {
                RubiesLotteryForm F = new RubiesLotteryForm(this);
                if (F.ShowDialog(this) != DialogResult.OK)
                    return;
            }
            Game.StartGame(true, lottery);
            Turn = new GameTable.Turn(Game.PlayersOf(Team.Light)[0], DF);
        }
        private void Start(string turnPlayer)
        {
            Game.StartGame(false, false);
            Turn = new GameTable.Turn(Game.FindPlayer(turnPlayer), DF);
        }

        private async void GameBox_MouseClick(object sender, MouseEventArgs e)
        {
            var ClickSector = Game.SectorFromPoint(e.Location);
            if (Game.IsDebug)
            {
                if (ClickSector != null && ClickSector.CanSelected)
                    (Game as DebugGame).SectorLocation = ClickSector.TblLocation;
            }
            else
            {
                if (ClickSector != null && ClickSector.CanSelected && ClickSector.Selected)
                    Turn = await Turn.SectorSelected(ClickSector);
            }
        }
        private void GameBox_MouseMove(object sender, MouseEventArgs e)
        {
            Game.DeselectedSectors();
            var UnderMouseSector = Game.SectorFromPoint(e.Location);
            if (Game.IsDebug)
            {
                Game.ClearSelectedSectors();
                if (UnderMouseSector != null)
                    UnderMouseSector.CanSelected = true;
            }
            else
            {
                if (UnderMouseSector != null && UnderMouseSector.CanSelected)
                    UnderMouseSector.Selected = true;
            }

            int Y = e.Y + GameBox.Top + GameBackgroundBox.Top;
            if (Y < 50)
                TableMove(1);
            else if (Y > Bottom - 150)
                TableMove(-1);
        }
        private void TableMove(int delta)
        {
            if (GameBackgroundBox.Top < 0 || GameBackgroundBox.Bottom > Bottom - (50 + Height - ClientSize.Height))
                GameBackgroundBox.Location += new Size(0, delta * 5);
            if (GameBackgroundBox.Top > 0)
                GameBackgroundBox.Location = new Point(GameBackgroundBox.Location.X, 0);
            else if (GameBackgroundBox.Bottom < Bottom - (50 + Height - ClientSize.Height))
                GameBackgroundBox.Location = new Point(GameBackgroundBox.Location.X, Bottom - (50 + Height - ClientSize.Height) - GameBackgroundBox.Height);
        }
        private void GameBox_MouseWheel(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(e.Delta.ToString());
            TableMove(e.Delta / 60);
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Game.Dispose();
            MainForm.GetMainForm.Show();
        }
    }
}