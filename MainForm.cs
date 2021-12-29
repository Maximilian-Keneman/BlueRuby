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
    public partial class MainForm : Form
    {
        public Settings Settings { get; }
        public static string ExecutablePath
        {
            get
            {
                string[] path = Application.ExecutablePath.Split('\\');
                return path.Take(path.Length - 1).Aggregate((D0, D1) => D0 + '\\' + D1);
            }
        }

        public MainForm()
        {
            InitializeComponent();
            /*/
            var myPath = new System.Drawing.Drawing2D.GraphicsPath();
            myPath.AddEllipse(0, 0, button1.Width, button1.Height);
            Region myRegion = new Region(myPath);
            button1.Region = myRegion;
            //*/
            Settings = new Settings();
            string[] Texts = Settings.Localization[LocalizationKeys.MainForm].Split('_');
            Text = Texts[0];
            QuickStartButton.Text = Texts[1];
            StartButton.Text = Texts[2];
            AnimationTimer.Start();
        }

        public new void Show()
        {
            base.Show();
            AnimationTimer.Start();
        }
        public new void Hide()
        {
            base.Hide();
            AnimationTimer.Stop();
        }

        private void QuickStartButton_Click(object sender, EventArgs e)
        {
            StartForm.QuickStart(Settings.StartParams);
            Hide();
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            StartForm F = new StartForm();
            if (F.ShowDialog() == DialogResult.OK)
                Hide();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm F = new SettingsForm();
            F.Show(this);
            Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GameForm F = new GameForm(new GameTable.GameData(new Player[0],
                                                             new Deckmode("Debug",
                                                                          new Dictionary<Type, int>()
                                                                          {
                                                                              { typeof(DragonKhiragir), 0 },
                                                                              { typeof(WizardCard), 0 },
                                                                              { typeof(SpellMagicFire), 0 },
                                                                              { typeof(ShieldTaygarol), 0 },
                                                                              { typeof(TenGoldCard), 0 },
                                                                              { typeof(FiveGoldCard), 0 }
                                                                          }, 0, 0)));
            F.DebugLoading();
            Hide();
        }

        public static MainForm GetMainForm => Application.OpenForms.OfType<MainForm>().Single();

        private bool UpDown = true;
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            BackgroundBox.Location = new Point(0, BackgroundBox.Location.Y + (UpDown ? -1 : 1));
            if (UpDown && BackgroundBox.Bottom < 600)
                UpDown = false;
            else if (!UpDown && BackgroundBox.Location.Y > 0)
                UpDown = true;
        }
    }
}