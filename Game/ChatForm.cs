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
    public partial class ChatForm : Form
    {
        private Player PlayerOwner;

        public ChatForm(Player owner, Player[] players)
        {
            InitializeComponent();
            Text = owner.Name;
            PlayerOwner = owner;
            comboBox1.Items.AddRange(players.Where(P => P != owner).Select(P => P.Name).ToArray());
            comboBox1.SelectedIndex = comboBox1SelectedIndex;
        }

        private int comboBox1SelectedIndex = 1;
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2)
                comboBox1.SelectedIndex = comboBox1SelectedIndex;
            else
                comboBox1SelectedIndex = comboBox1.SelectedIndex;
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && e.KeyChar == '\r')
            {
                string msg = textBox1.Text;
                textBox1.Text = "";
                ShowMessage(PlayerOwner, msg, comboBox1.Text);
            }
        }

        public enum comboBox1Item
        {
            All,
            Team,
            Personal
        }
        public void SendMesseage(Player sender, comboBox1Item item, string msg, Player personal = null)
        {
            string reciver = item switch
            {
                comboBox1Item.All => "All",
                comboBox1Item.Team => "Team",
                comboBox1Item.Personal => personal?.Name ?? throw new Exception("Need personal"),
                _ => throw new Exception("wrong state")
            };
            if (item == comboBox1Item.All ||
                item == comboBox1Item.Team && PlayerOwner.Team == sender.Team ||
                item == comboBox1Item.Personal && PlayerOwner.Name == reciver)
                ShowMessage(sender, msg, reciver);
        }

        private void ShowMessage(Player sender, string msg, string reciver)
        {
            richTextBox1.Text += $"\n[{DateTime.Now.ToShortTimeString()}] [{reciver}] from [{sender.Name}]:{msg}";
        }
    }
}
