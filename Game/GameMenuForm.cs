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
    public partial class GameMenuForm : Form
    {
        public GameMenuForm()
        {
            InitializeComponent();
            string[] texts = MainForm.GetMainForm.Settings.Localization[LocalizationKeys.GameMenu].Split('_');
            Text = texts[0];
            SaveButton.Text = texts[1];
            LoadButton.Text = texts[2];
            ExitButton.Text = texts[3];
            GameTable.Turn.TurnBegun += (sender, e) =>
            {
                SaveButton.Enabled = !(sender as GameTable.Turn).IsTurnStart;
                LoadButton.Enabled = !(sender as GameTable.Turn).IsTurnStart;
            };
        }
        public void DeckLoad()
        {
            Size delta = new Size(2, 0);
            GameTable game = (Owner as GameForm).Game;
            CardsDeckBox.Image = Images.SetDeck(Enumerable.Repeat(Card.CloseTexture, game.Deck.Count).ToArray(), CardsDeckBox.Size, delta);
            DeckCountLabel.Text = game.Deck.Count.ToString();
            game.Deck.CollectionChanged += (sender, e) =>
            {
                CardsDeckBox.Image = Images.SetDeck(Enumerable.Repeat(Card.CloseTexture, (sender as System.Collections.ObjectModel.ObservableCollection<Card>).Count).ToArray(), CardsDeckBox.Size, delta);
                DeckCountLabel.Text = (sender as System.Collections.ObjectModel.ObservableCollection<Card>).Count.ToString();
            };
            if (game.IsInfiniteLiveDeck)
            {
                EnergyDeckBox.Image = new Bitmap(LiveCard.CloseTexture, Card.CardSize);
                LiveCountLabel.Text = "∞";
                LiveCountLabel.Font = new Font(LiveCountLabel.Font.FontFamily, 13);
            }
            else
            {
                if (game.IsNullLiveDeck)
                {
                    EnergyDeckBox.Image = Images.SetTransparentImage(EnergyDeckBox.Size);
                    LiveCountLabel.Text = "0";
                }
                else
                {
                    EnergyDeckBox.Image = Images.SetDeck(Enumerable.Repeat(LiveCard.CloseTexture, game.LiveDeckCount).ToArray(), EnergyDeckBox.Size, delta);
                    LiveCountLabel.Text = game.LiveDeckCount.ToString();
                    game.LiveDeckChanged += (sender, e) =>
                    {
                        EnergyDeckBox.Image = Images.SetDeck(Enumerable.Repeat(LiveCard.CloseTexture, (sender as GameTable).LiveDeckCount).ToArray(), EnergyDeckBox.Size, delta);
                        LiveCountLabel.Text = (sender as GameTable).LiveDeckCount.ToString();
                    };
                }
            }
        }

        private void Save()
        {
            SaveLoadDialog F = new SaveLoadDialog(SaveLoadDialog.DialogMode.Save);
            F.ShowDialog(this);
        }
        private void SaveButton_Click(object sender, EventArgs e)
            => Save();
        private void LoadButton_Click(object sender, EventArgs e)
        {
            SaveLoadDialog F = new SaveLoadDialog(SaveLoadDialog.DialogMode.Load);
            if (F.ShowDialog(this) == DialogResult.OK)
            {
                if ((Owner as GameForm).Game.Start)
                {
                    var locale = MainForm.GetMainForm.Settings.Localization;
                    switch (MessageBox.Show(locale[LocalizationKeys.LoadSaveQuestion],
                                            locale[LocalizationKeys.GameMenu].Split('_')[2],
                                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            Save();
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }
                Owner.Close();
                MainForm.GetMainForm.Hide();
                StartForm.StartGame(F.Data, F.Data.TurnPlayer);
                /*
                GameForm GF = new GameForm(F.Data);
                GF.FormsShow();
                GF.Start(F.Data.TurnPlayer);
                */
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if ((Owner as GameForm).Game.Start)
            {
                var locale = MainForm.GetMainForm.Settings.Localization;
                switch (MessageBox.Show(locale[LocalizationKeys.ExitSaveQuestion],
                                        locale[LocalizationKeys.GameMenu].Split('_')[3],
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        Save();
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }
            Owner.Close();
        }
    }
}
