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
    public partial class TextDialog : Form
    {
        private Localization Localization => MainForm.GetMainForm.Settings.Localization;
        public string OutName { get; private set; }

        public TextDialog()
        {
            InitializeComponent();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
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
        private void OkButton_Click(object sender, EventArgs e)
        {
            OutName = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
