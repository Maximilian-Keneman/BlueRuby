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
    public partial class TeamForm : Form
    {
        public Team Team { get; }

        public TeamForm(Team team, GameForm game)
        {
            InitializeComponent();
            Team = team;
            Text = MainForm.GetMainForm.Settings.Localization[LocalizationKeys.Teams].Split('_')[team switch
            {
                Team.Light => 1,
                Team.Dark => 2,
                _ => 0
            }];
            List<Player> Players = game.Game.PlayersOf(team).ToList();
            for (int i = 0; i < Players.Count; i++)
            {
                var Card = new PlayerStatCard(Players[i]) { Location = new Point(5, 5 + 157 * i) };
                Controls.Add(Card);
            }
            GameTable.Turn.TurnPlayerChanged += Game_TurnPlayerChanged;
        }

        private void Game_TurnPlayerChanged(object sender, GameTable.Turn.TurnEventArgs e)
        {
            foreach (var stat in from Control C in Controls
                                 where C is PlayerStatCard
                                 select C as PlayerStatCard)
            {
                if ((e.Team == Team && stat.Name == e.TurnPlayer.Name) ||
                    (e.Team == Team.Wizard && (e.TurnPlayer as Wizard).Owner.Team == Team && stat.Name == (e.TurnPlayer as Wizard).Owner.Name))
                    stat.FillTurnBox(Color.Green);
                else if (e.LosePlayers.Any(P => P.Name == stat.Name))
                    stat.FillTurnBox(Color.Red);
                else
                    stat.FillTurnBox(Color.Transparent);
            }
        }
    }
}
