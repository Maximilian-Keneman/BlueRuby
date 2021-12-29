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
    public partial class SettingsForm : Form
    {
        private Settings Settings => MainForm.GetMainForm.Settings;
        public SettingsForm()
        {
            InitializeComponent();
            if (Settings.NoErrorLocales.Length > 0)
                LocalizationBox.Items.AddRange(Settings.NoErrorLocales);
            else
                LocalizationBox.Items.Add(Default.Localization);
            LocalizationBox.SelectedItem = Settings.Localization;

            LightNamesBox.Lines = Settings.StartParams.QuickStartNames[Team.Light];
            DarkNamesBox.Lines = Settings.StartParams.QuickStartNames[Team.Dark];
            PlayersCountNumeric.Value = Settings.StartParams.PlayersCount;
            NamesCheck();
            DeckmodeBox.Items.AddRange(Deckmode.Load());
            if (DeckmodeBox.Items.Count > 0)
                DeckmodeBox.SelectedIndex = 0;
            LotteryBox.Checked = Settings.StartParams.Lottery;
            CheckParams();

        }
        private void DefaultsSaveButton_Click(object sender, EventArgs e)
        {
            string LocalesPath = MainForm.ExecutablePath + "\\locdata";
            if (!File.Exists(LocalesPath + "\\Russian.loc"))
            {
                Localization.CreateLocalization(Default.Localization.ToDictionary(KT => KT.Key, KT => KT.Value),
                                                Default.Localization.Language, LocalesPath);
                MessageBox.Show("Default Localization saved.");
            }
            string ModeDatPath = Deckmode.ModesDatPath;
            if (!Deckmode.ParamsNames().Contains("Standart"))
            {
                string Params = "Standart" +
                                 Default.Deckmode.Deck.Select(V => $"\n{V.Key.Name}@{V.Value}").Aggregate((S0, S1) => S0 + S1) +
                                 "\nLiveCount@" + Default.Deckmode.LiveCount +
                                 "\nDeckLiveCount@" + Default.Deckmode.DeckLiveCount;
                if (File.Exists(ModeDatPath))
                    File.AppendAllLines(ModeDatPath, Params.Split('\n').Prepend("@").Select(S => HexConvert.InHex(S)));
                else
                    File.WriteAllLines(ModeDatPath, Params.Split('\n').Select(S => HexConvert.InHex(S)));
                MessageBox.Show("Default Mode saved.");
            }
        }

        private void LocalizationBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.ChangeSetting(LocalizationBox.SelectedItem);
        }
        private void CreateLocButton_Click(object sender, EventArgs e)
        {
            LocalizationForm F = new LocalizationForm(Settings.Locales);
            F.Show();
        }

        private void NamesBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '@')
                e.Handled = true;
            return;
        }
        private void NamesCount_Changed(object sender, EventArgs e)
        {
            NamesCheck();
        }

        private void DeckmodeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeckMode = DeckmodeBox.SelectedIndex != -1;
        }

        private bool _namesCount = false;
        private bool _deckMode = false;
        private bool NamesCount
        {
            get => _namesCount;
            set
            {
                _namesCount = value;
                CheckParams();
            }
        }
        private bool DeckMode
        {
            get => _deckMode;
            set
            {
                _deckMode = value;
                CheckParams();
            }
        }
        private void NamesCheck()
        {
            NamesCount = !(LightNamesBox.Lines.Length < PlayersCountNumeric.Value / 2 ||
                           DarkNamesBox.Lines.Length < PlayersCountNumeric.Value / 2);
        }
        private void CheckParams()
            => SaveParamsButton.Enabled = NamesCount && DeckMode;

        private void SaveParamsButton_Click(object sender, EventArgs e)
        {
            Settings.ChangeSetting(new Settings.QuickStartParams(LightNamesBox.Lines,
                                                                 DarkNamesBox.Lines,
                                                                 (int)PlayersCountNumeric.Value,
                                                                 DeckmodeBox.SelectedItem as Deckmode,
                                                                 !LotteryBox.Checked));
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            (Owner as MainForm).Show();
        }
    }

    public class Settings
    {
        #region Localization
        public Localization Localization { get; private set; }
        public Localization[] Locales { get; }
        public Localization[] NoErrorLocales => Locales.Where(L => !L.HaveError).ToArray();
        #endregion

        public class QuickStartParams
        {
            public Dictionary<Team, string[]> QuickStartNames { get; } 
            public int PlayersCount { get; }
            public Deckmode Deckmode { get; }
            public bool Lottery { get; }

            public QuickStartParams(string[] lightNames, string[] darkNames,
                                    int playersCount, Deckmode deckmode, bool lottery)
            {
                QuickStartNames = new Dictionary<Team, string[]>()
                {
                    { Team.Light, lightNames },
                    { Team.Dark, darkNames }
                };
                PlayersCount = playersCount;
                Deckmode = deckmode;
                Lottery = lottery;
            }
        }
        public QuickStartParams StartParams { get; private set; }

        public Settings()
        {
            #region Localization
            Locales = Directory.EnumerateFiles(MainForm.ExecutablePath + "\\locdata", "*.loc").Select(P => new Localization(P)).ToArray();
            Default.Localization.LocalizationCheck();
            if (Default.Localization.HaveError)
                throw new Exception("Нет стандартного значение для " + string.Join(", ", Default.Localization.ErrorKeys));
            Localization = NoErrorLocales.Length > 0 ? NoErrorLocales[0] : Default.Localization;
            #endregion
            StartParams = Default.StartParams;
        }

        public void ChangeSetting(params object[] settings)
        {
            foreach (object setting in settings)
            {
                if (setting is Localization loc)
                    Localization = loc;
                else if (setting is QuickStartParams ps)
                    StartParams = ps;
                else
                    throw new Exception(setting.GetType().Name + " no find in Settings");
            }
        }
    }
}
