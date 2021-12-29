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
    public partial class LoadingForm : Form
    {
        public LoadingForm(int FullCount, object startState, StartForm form)
        {
            InitializeComponent();
            progressBar1.Maximum = FullCount;
            progressBar1.Value = 0;
            this.startState = startState;
            this.form = form;
        }

        private object startState;
        private StartForm form;

        public void Start(GameTable game)
        {
            backgroundLoading.RunWorkerAsync(game);
        }
        private void BackgroundLoading_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = new WorldParts.BotMaps(e.Argument as GameTable, backgroundLoading);
        }
        private void BackgroundLoading_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = (int)e.UserState;
        }
        private void BackgroundLoading_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            (Owner as GameForm).LoadingComplete(e.Result as WorldParts.BotMaps, startState, form);
            Close();
        }
    }
}
