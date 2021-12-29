using System;
using System.Collections;
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
    public partial class LocalizationForm : Form
    {
        
        public LocalizationForm(Localization[] locales)
        {
            InitializeComponent();
            for (int i = 0; i < LocalizationKeys.Keys.Length; i++)
            {
                string key = LocalizationKeys.Keys[i];
                Label key_label = new Label()
                {
                    AutoSize = true,
                    Location = new Point(12, 35 + i * 26),
                    Name = key + "_label",
                    Text = key
                };
                TextBox key_Box = new TextBox()
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Location = new Point(164, 32 + i * 26),
                    Name = key + "_Box",
                    Size = Name_Box.Size
                };
                Label key_lacalelabel = new Label()
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    AutoSize = true,
                    Location = new Point(170 + key_Box.Width, 35 + i * 26),
                    Name = key + "_localelabel",
                    Text = Default.Localization[key]
                };
                Controls.AddRange(new Control[] { key_label, key_Box, key_lacalelabel });
                if (LocalizationKeys.KeysHaveBraces.ContainsKey(key))
                {
                    toolTip1.SetToolTip(key_label, $"Текст должен содержать {LocalizationKeys.KeysHaveBraces[key]} '{{}}'");
                    toolTip1.SetToolTip(key_Box, $"Текст должен содержать {LocalizationKeys.KeysHaveBraces[key]} '{{}}'");
                }
                if (LocalizationKeys.KeysHaveUnderlines.ContainsKey(key))
                {
                    toolTip1.SetToolTip(key_label, $"Текст должен содержать {LocalizationKeys.KeysHaveUnderlines[key] - 1} '_'");
                    toolTip1.SetToolTip(key_Box, $"Текст должен содержать {LocalizationKeys.KeysHaveUnderlines[key] - 1} '_'");
                }
            }
            SaveButton.Location = new Point(12, 32 + LocalizationKeys.Keys.Length * 26);
            LocaleBox.Items.AddRange(locales);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Localization.CreateLocalization(Controls.OfType<TextBox>()
                                                    .Where(B => B.Name != "Name_Box")
                                                    .Select(B => (Name: B.Name.Split('_')[0], B.Text))
                                                    .ToDictionary(B => B.Name, B => B.Text),
                                            Name_Box.Text, MainForm.ExecutablePath);
        }

        private void LocaleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var texts in LocaleBox.SelectedItem as Localization)
                Controls[texts.Key + "_localelabel"].Text = texts.Value;
            button1.Enabled = LocaleBox.SelectedIndex != -1;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Controls["Name_Box"].Text = (LocaleBox.SelectedItem as Localization).Language;
            foreach (var texts in LocaleBox.SelectedItem as Localization)
                Controls[texts.Key + "_Box"].Text = texts.Value;
        }
    }
    public static class LocalizationKeys
    {
        public static string[] Keys =>
            typeof(LocalizationKeys).GetProperties().Where(P => P.PropertyType == typeof(string)).Select(P => P.Name).ToArray();
        public static Dictionary<string, int> KeysHaveBraces => new Dictionary<string, int>()
        {
            { RepeatNamesError, 2 },
            { InvalidCharError, 1 },
            { LightTeamCountError, 1 },
            { DarkTeamCountError, 1 },
            { PlayerCanAttack, 1 },
            { PlayerLoseTurn, 1 },
            { TeamWin, 1 }
        };
        public static Dictionary<string, int> KeysHaveUnderlines => new Dictionary<string, int>()
        {
            { MainForm, 5 },
            { StartForm, 8 },
            { Gamemodes, 2 },
            { SaveLoadForm, 8 },
            { Teams, 3 },
            { GameMenu, 4 },
            { Interactives, 4 },
            { Cards, 6 }
        };

        #region Keys
        public static string MainForm => "MainForm";
        public static string StartForm => "StartForm";
        public static string Gamemodes => "Gamemodes";
        public static string NullPlayersError => "NullPlayersError";
        public static string UnevenCountError => "UnevenCountError";
        public static string PlayerVSPlayerError => "PlayerVSPlayerError";
        public static string EmptyNameError => "EmptyNameError";
        public static string RepeatNamesError => "RepeatNamesError";
        public static string InvalidCharError => "InvalidCharError";
        public static string Teams => "Teams";
        public static string LightTeamCountError => "LightTeamCountError";
        public static string DarkTeamCountError => "DarkTeamCountError";
        public static string Cards => "Cards";
        public static string LiveCompilation => "LiveCompilation";
        public static string SaveLoadForm => "SaveLoadForm";
        public static string LoadSaveQuestion => "LoadSaveQuestion";
        public static string ExitSaveQuestion => "ExitSaveQuestion";
        public static string GameMenu => "GameMenu";
        public static string DiceForm => "DiceForm";
        public static string UseDragon => "UseDragon";
        public static string UseMagicFire => "UseMagicFire";
        public static string Interactives => "Interactives";
        public static string NoMoney => "NoMoney";
        public static string NoSmallMoney => "NoSmallMoney";
        public static string MagicTowerQuestion => "MagicTowerQuestion";
        public static string FortressQuestion => "FortressQuestion";
        public static string CatacombQuestion => "CatacombQuestion";
        public static string CatacombMoneyQuestion => "CatacombMoneyQuestion";
        public static string ShipQuestion => "ShipQuestion";
        public static string DeckLose => "DeckLose";
        public static string MagicTowerNoDoubleEnter => "MagicTowerNoDoubleEnter";
        public static string FortressNoDoubleEnter => "FortressNoDoubleEnter";
        public static string NoDoubleWizard => "NoDoubleWizard";
        public static string PlayerAttack => "PlayerAttack";
        public static string PlayerCanAttack => "PlayerCanAttack";
        public static string PlayerLoseTurn => "PlayerLoseTurn";
        public static string TeamWin => "TeamWin";
        #endregion
    }

    public class Localization : IEnumerable<KeyValuePair<string, string>>
    {
        public string Language { get; }

        private Dictionary<string, string> KeyTexts;
        public string this[string key] => KeyTexts[key];
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => KeyTexts.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => KeyTexts.GetEnumerator();

        public bool HaveError { get; private set; }
        public List<string> ErrorKeys { get; private set; }

        public Localization(string path)
        {
            string[] filePath = path.Split('\\');
            Language = filePath.Last().Split('.')[0];
            string[] CodingKeyTexts = File.ReadAllLines(path);
            try
            {
                KeyTexts = CodingKeyTexts.Select(v => HexConvert.OutHex(v).Split('@')).ToDictionary(V => V[0], V => V[1]);
                LocalizationCheck();
            }
            catch (Exception)
            {
                HaveError = true;
                KeyTexts = new Dictionary<string, string>();
                Language += "@Error";
            }
        }
        public Localization(string language, Dictionary<string, string> keyTexts)
        {
            Language = language;
            KeyTexts = keyTexts;
        }
        public static Localization CreateLocalization(Dictionary<string, string> keyTexts, string language, string path)
        {
            string filePath = path + "\\" + language + ".loc";
            if (!File.Exists(filePath))
            {
                var stream = File.Create(filePath);
                stream.Close();
            }
            File.WriteAllLines(filePath, keyTexts.Select(v => HexConvert.InHex(v.Key + '@' + v.Value)));
            return new Localization(language, keyTexts);
        }
        
        public void LocalizationCheck()
        {
            List<string> ErrKeys = new List<string>();
            foreach (string key in LocalizationKeys.Keys)
            {
                if (!this.Select(KT => KT.Key).Contains(key))
                    ErrKeys.Add(key);
            }
            foreach (var texts in this)
            {
                if (texts.Value == "")
                    ErrKeys.Add(texts.Key);
                if (LocalizationKeys.KeysHaveUnderlines.ContainsKey(texts.Key) &&
                    texts.Value.Split('_').Length != LocalizationKeys.KeysHaveUnderlines[texts.Key])
                    ErrKeys.Add(texts.Key);
                if (LocalizationKeys.KeysHaveBraces.ContainsKey(texts.Key) &&
                    texts.Value.Count(Ch => "{}".Contains(Ch)) / 2 != LocalizationKeys.KeysHaveBraces[texts.Key])
                    ErrKeys.Add(texts.Key);
            }
            if (ErrKeys.Count > 0)
            {
                ErrorKeys = ErrKeys;
                HaveError = true;
            }
            else
            {
                ErrorKeys = null;
                HaveError = false;
            }
        }

        public override string ToString() => Language;
    }
}
