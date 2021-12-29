using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueRuby
{
    public partial class PlayerPanel : UserControl
    {
        private Localization Localization => MainForm.GetMainForm.Settings.Localization;

        public PlayerPanel()
        {
            InitializeComponent();
            //Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TeamBox.Items.AddRange(Localization[LocalizationKeys.Teams].Split('_'));
            TeamBox.SelectedIndex = 0;
        }

        public event EventHandler NameChanged;
        public event EventHandler TeamChanged;
        public event EventHandler PlayerDeleted;

        private void NameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '@')
            {
                string[] ErrorMes = Localization[LocalizationKeys.InvalidCharError].Split("{}".ToCharArray());
                ErrorMes[1] = "'@'";
                MessageBox.Show(ErrorMes.Aggregate((S0, S1) => S0 + S1));
                e.Handled = true;
            }
            else
                return;
        }
        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            NameChanged?.Invoke(sender, e);
        }
        private void TeamBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TeamChanged?.Invoke(sender, e);
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Dispose();
            PlayerDeleted?.Invoke(sender, e);
        }

        public void NameError(string Message)
        {
            NameBox.BackColor = Color.Red;
            NamesError.SetIconAlignment(NameBox, ErrorIconAlignment.MiddleLeft);
            NamesError.SetError(NameBox, Message);
        }
        public void TeamError(string Message)
        {
            TeamCountError.SetIconAlignment(TeamBox, ErrorIconAlignment.MiddleRight);
            TeamCountError.SetError(TeamBox, Message);
        }
        public void NameErrorClear()
        {
            NamesError.Clear();
            NameBox.BackColor = SystemColors.Window;
        }
        public void TeamErrorClear()
        {
            TeamCountError.Clear();
        }

        public string PlayerName => NameBox.Text;
        public Team PlayerTeam => TeamBox.SelectedIndex switch
        {
            1 => Team.Light,
            2 => Team.Dark,
            _ => Team.Fantom
        };
        public Player GetPlayer(int liveCount)
        {
                return new Player(PlayerName, PlayerTeam, liveCount, BotCheck.Checked);
        }
    }
}
