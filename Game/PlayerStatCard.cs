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
    public partial class PlayerStatCard : UserControl
    {
        public PlayerStatCard(Player player)
        {
            InitializeComponent();
            Name = NameLabel.Text = player.Name;
            EnergyBox.Image = Images.SetDeck(Enumerable.Repeat(LiveCard.OpenTexture, player.Live).ToArray(), EnergyBox.Size, new Size(10, 0));
            LiveCount.Text = player.Live.ToString();
            CardsBox.Image = Images.SetDeck(player.GetDeck(), CardsBox.Size, new Size(10, 0));
            GoldCount.Text = player.Gold.ToString();
            BlueRubyCheck(player.BlueRuby);
            BlueRubyCount.Text = player.BlueRuby.Count.ToString();
            TurnBox.Image = Images.SetSquare(Color.Transparent, TurnBox.Size);
            player.LiveChanged += Player_LiveChanged;
            player.DeckChanged += Player_DeckChanged;
            player.BlueRubyListChanged += Player_BlueRubyTaked;
        }

        private void BlueRubyCheck(List<BlueRubyCard> Rubies)
        {
            Image img;
            if (Rubies.Count > 0)
            {
                int i = Rubies.FindIndex(R => R.IsReal);
                if (i != -1 && Rubies[i].IsOpen)
                    img = Rubies[i].Texture;
                else
                {
                    i = Rubies.FindIndex(R => !R.IsOpen);
                    if (i != -1)
                        img = Rubies[i].Texture;
                    else
                        img = Rubies[0].Texture;
                }
            }
            else
                img = Images.SetTransparentImage(BlueRubyBox.Size);
            BlueRubyBox.Image = new Bitmap(img, BlueRubyBox.Size);
        }
        private void Player_BlueRubyTaked(object sender, EventArgs e)
        {
            var BRs = (sender as Player).BlueRuby;
            BlueRubyCount.Text = BRs.Count.ToString();
            BlueRubyCheck(BRs);
        }
        private void Player_LiveChanged(object sender, EventArgs e)
        {
            int Lives = (sender as Player).Live;
            EnergyBox.Image = Lives > 0 ?
                Images.SetDeck(Enumerable.Repeat(LiveCard.OpenTexture, Lives).ToArray(), EnergyBox.Size, new Size(10, 0)) :
                Images.SetTransparentImage(EnergyBox.Size);
            LiveCount.Text = Lives.ToString();
            if ((sender as Player).IsDead)
            {
                LiveCount.Text = "Dead";
                BackColor = Color.SlateGray;
            }
        }
        private void Player_DeckChanged(object sender, EventArgs e)
        {
            CardsBox.Image = Images.SetDeck((sender as Player).GetDeck(), CardsBox.Size, new Size(10, 0));
            GoldCount.Text = (sender as Player).Gold.ToString();
        }
        public void FillTurnBox(Color color)
        {
            TurnBox.Image = Images.SetSquare(color, TurnBox.Size);
        }
    }
}
