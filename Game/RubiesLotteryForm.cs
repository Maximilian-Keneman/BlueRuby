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
    public partial class RubiesLotteryForm : Form
    {
        private Player[] DarkPlayers;
        private Player[] LightPlayers;

        private Player SelectPlayer => SelectTeam == Team.Light ? LightPlayers[index] : DarkPlayers[index];
        private int index = 0;
        private Team SelectTeam = Team.Light;

        private int Lucky = -1;

        public RubiesLotteryForm(Form owner)
        {
            InitializeComponent();
            Owner = owner;
            LightPlayers = (Owner as GameForm).Game.PlayersOf(Team.Light);
            DarkPlayers = (Owner as GameForm).Game.PlayersOf(Team.Dark);
            int count = LightPlayers.Length + DarkPlayers.Length;
            int[] rubies = null;
            switch (count)
            {
                case 2:
                    rubies = new int[] { 1, 2 };
                    Lucky = Expansion.Rnd.Next() % 2 + 1;
                    break;
                case 4:
                    rubies = new int[] { 1, 2, 3, 4 };
                    Lucky = Expansion.Rnd.Next() % 4 + 1;
                    break;
                case 6:
                    rubies = new int[] { 1, 2, 3, 5, 6, 7 };
                    Lucky = Expansion.Rnd.Next() % 6 + 1;
                    if (Lucky > 3)
                        Lucky++;
                    break;
                case 8:
                    rubies = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                    Lucky = Expansion.Rnd.Next() % 8 + 1;
                    break;
            }
            for (int i = 0; i < rubies.Length; i++)
            {
                PictureBox RubyBox = Controls["Ruby" + rubies[i]] as PictureBox;
                RubyBox.Visible = true;
                RubyBox.Image = new Bitmap(BlueRubyCard.CloseTexture, RubyBox.Size);
            }
            NameLabel.Text = SelectPlayer.Name;
        }

        private void RubySelect(object sender, EventArgs e)
        {
            PictureBox senderBox = sender as PictureBox;
            SelectPlayer.TakeBlueRuby(new BlueRubyCard(Convert.ToInt32(senderBox.Name.Last().ToString()) == Lucky));
            senderBox.Click -= RubySelect;
            senderBox.Image = Images.SetSquare(Color.Gray, senderBox.Size);
            if (SelectTeam == Team.Light)
                SelectTeam = Team.Dark;
            else if (SelectTeam == Team.Dark && index < DarkPlayers.Length - 1)
            {
                SelectTeam = Team.Light;
                index++;
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }
            NameLabel.Text = SelectPlayer.Name;
        }
    }
}
