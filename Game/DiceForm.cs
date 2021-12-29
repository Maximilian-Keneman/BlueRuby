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
    public partial class DiceForm : Form
    {
        private Localization Localization => MainForm.GetMainForm.Settings.Localization;
        private Image[] DiceNumbers = new Image[6];
        public DiceForm()
        {
            InitializeComponent();
            string[] Texts = Localization[LocalizationKeys.DiceForm].Split('_');
            Text = Texts[0];
            RollButton.Text = Texts[1];
            Point[] DNPoints = new Point[6]
            {
                new Point(259, 777),
                new Point(259, 518),
                new Point(0, 259),
                new Point(259, 0),
                new Point(518, 259),
                new Point(259, 259)
            };
            for (int i = 0; i < 6; i++)
                DiceNumbers[i] = Images.GetFragment(Properties.Resources.dice, new Size(88, 88), new Rectangle(DNPoints[i].X, DNPoints[i].Y, 265, 265));
            DiceCheckBox.Image = DiceNumbers[Dice.DiceCheck()];
        }

        public enum ActiveType
        {
            Move,
            Attack,
            InCatacomb,
            FromCatacomb
        }
        private ActiveType Active;
        private void DiceActivate(ActiveType active, bool HaveNeedCard)
        {
            Active = active;
            CardCheckBox.Checked = false;
            CardCheckBox.Visible = false;
            CardCheckBox.Enabled = true;
            if (HaveNeedCard)
                switch (active)
                {
                    case ActiveType.Move:
                        CardCheckBox.Text = Localization[LocalizationKeys.UseDragon];
                        CardCheckBox.Visible = true;
                        break;
                    case ActiveType.Attack:
                        CardCheckBox.Text = Localization[LocalizationKeys.UseMagicFire];
                        CardCheckBox.Visible = true;
                        break;
                }
        }
        public void MoveDiceActivate(bool HaveNeedCard, Player turnPlayer)
        {
            DiceActivate(ActiveType.Move, HaveNeedCard);
            if (turnPlayer.IsBot)
            {
                if (HaveNeedCard)
                    CardCheckBox.Checked = turnPlayer.Bot.GetCardAnswer(Bot.CardTurnCase.MoveCardDice,
                                                                        turnPlayer.OwnerGame.Maps.GetMap(turnPlayer.Team, false, false)
                                                                        .StartPoint.StepCount(turnPlayer.TblLocation));
                RollButton_Click(RollButton, EventArgs.Empty);
            }
            else
                RollButton.Enabled = true;
        }
        public void AttackDiceActivate(bool HaveNeedCard, Player turnPlayer, int selectedDirection)
        {
            DiceActivate(ActiveType.Attack, HaveNeedCard);
            if (turnPlayer.IsBot)
            {
                if (HaveNeedCard)
                {
                    int MinDistance()
                    {
                        for (int I = 1; I <= 6; I++)
                        {
                            Point P = turnPlayer.TblLocation + (selectedDirection switch
                            {
                                0 => new Size(-1, 0),
                                1 => new Size(0, 1),
                                2 => new Size(1, 0),
                                3 => new Size(0, -1),
                                _ => throw new NumberException(nameof(selectedDirection), 0, 3)
                            }).Multiple(I);
                            if (turnPlayer.OwnerGame[P].GetPlayers().Any())
                                return I;
                        }
                        return int.MaxValue;
                    }
                    CardCheckBox.Checked = turnPlayer.Bot.GetCardAnswer(Bot.CardTurnCase.AttackCardDice, MinDistance());
                }
                RollButton_Click(RollButton, EventArgs.Empty);
            }
            else
                RollButton.Enabled = true;
        }
        public void CatacombDiceActivate(ActiveType active, Player turnPlayer)
        {
            DiceActivate(active, false);
            if (turnPlayer.IsBot)
                RollButton_Click(RollButton, EventArgs.Empty);
            else
                RollButton.Enabled = true;
        }
        public void DiceActivate(ActiveType active, bool HaveNeedCard, Player turnPlayer, int selectedDirection)
        {
            Active = active;
            CardCheckBox.Checked = false;
            CardCheckBox.Visible = false;
            CardCheckBox.Enabled = true;
            if (HaveNeedCard)
                switch (active)
                {
                    case ActiveType.Move:
                        CardCheckBox.Text = Localization[LocalizationKeys.UseDragon];
                        CardCheckBox.Visible = true;
                        break;
                    case ActiveType.Attack:
                        CardCheckBox.Text = Localization[LocalizationKeys.UseMagicFire];
                        CardCheckBox.Visible = true;
                        break;
                }
            if (turnPlayer.IsBot)
            {
                if (HaveNeedCard)
                    switch (active)
                    {
                        case ActiveType.Move:
                            CardCheckBox.Checked = turnPlayer.Bot.GetCardAnswer(Bot.CardTurnCase.MoveCardDice,
                                                                                turnPlayer.OwnerGame.Maps.GetMap(turnPlayer.Team, false, false)
                                                                                .StartPoint.StepCount(turnPlayer.TblLocation));
                            break;
                        case ActiveType.Attack:
                            int MinDistance()
                            {
                                for (int I = 1; I <= 6; I++)
                                {
                                    Point P = turnPlayer.TblLocation + (selectedDirection switch
                                    {
                                        0 => new Size(-1, 0),
                                        1 => new Size(0, 1),
                                        2 => new Size(1, 0),
                                        3 => new Size(0, -1),
                                        _ => throw new NumberException(nameof(selectedDirection), 0, 3)
                                    }).Multiple(I);
                                    if (turnPlayer.OwnerGame[P].GetPlayers().Any())
                                        return I;
                                }
                                return int.MaxValue;
                            }
                            CardCheckBox.Checked = turnPlayer.Bot.GetCardAnswer(Bot.CardTurnCase.AttackCardDice, MinDistance());
                            break;
                    }
                RollButton_Click(RollButton, EventArgs.Empty);
            }
            else
                RollButton.Enabled = true;
        }
        public async void FastResult(ActiveType active, int dice, bool UseCard = false)
        {
            await Task.Delay(1000);
            switch (active)
            {
                case ActiveType.Move:
                    (Owner as GameForm).Turn = await (Owner as GameForm).Turn.GoToMove(dice, UseCard);
                    break;
                case ActiveType.Attack:
                    (Owner as GameForm).Turn = await (Owner as GameForm).Turn.Attack(dice, UseCard);
                    break;
                case ActiveType.InCatacomb:
                    (Owner as GameForm).Turn = (Owner as GameForm).Turn.GoToCatacomb(dice);
                    break;
                case ActiveType.FromCatacomb:
                    (Owner as GameForm).Turn = (Owner as GameForm).Turn.GoFromCatacomb(dice);
                    break;
            }

        }
        private async void RollButton_Click(object sender, EventArgs e)
        {
            RollButton.Enabled = false;
            CardCheckBox.Enabled = false;
            await Task.Delay(100);
            if (Active != ActiveType.Attack)
                (Owner as GameForm).Turn.TurnStart();
            int[] check = Dice.DiceRoll();
            for (int i = 0; i < check.Length; i++)
            {
                DiceCheckBox.Image = DiceNumbers[check[i]];
                await Task.Delay(100);
            }
            switch (Active)
            {
                case ActiveType.Move:
                    (Owner as GameForm).Turn = await (Owner as GameForm).Turn.GoToMove(check.Last() + 1, CardCheckBox.Checked);
                    break;
                case ActiveType.Attack:
                    (Owner as GameForm).Turn = await (Owner as GameForm).Turn.Attack(check.Last() + 1, CardCheckBox.Checked);
                    break;
                case ActiveType.InCatacomb:
                    (Owner as GameForm).Turn = (Owner as GameForm).Turn.GoToCatacomb(check.Last() + 1);
                    break;
                case ActiveType.FromCatacomb:
                    (Owner as GameForm).Turn = (Owner as GameForm).Turn.GoFromCatacomb(check.Last() + 1);
                    break;
            }
        }
    }

    public static class Dice
    {
        public static int[] DiceRoll()
        {
            int v = 0;
            while (v == 0)
                v = (int)Math.Pow(DateTime.UtcNow.Millisecond, Expansion.Rnd.NextDouble() * 4);
            List<int> Row = new List<int>();
            while (v != 0)
            {
                Row.Add(Math.Abs(v % 6));
                v /= 6;
            }
            Row.RemoveAt(Row.Count - 1);
            Row.Reverse();
            Row.Add(DiceCheck());
            return Row.ToArray();
        }
        public static int DiceCheck() => Expansion.Rnd.Next(0, 6);
    }
}
