using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueRuby
{
    public static class OnlinePlayer
    {
        public enum WaitState
        {
            Dice,
            Place,
            Question
        }

        public static object GetAnswer(WaitState state, Player player)
        {
            int data = -1;
            if (player.IsOnlinePlayer)
            {

            }
            return state switch
            {
                WaitState.Dice => data.ToString().ToCharArray().Select(c => int.Parse(c.ToString())).ToArray(),
                WaitState.Place => (data / 100, data % 100).ToPoint(),
                WaitState.Question => (DialogResult)Enum.GetValues(typeof(DialogResult)).GetValue(data),
                _ => null,
            };
        }
    }
}
