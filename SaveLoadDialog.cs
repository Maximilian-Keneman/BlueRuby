using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueRuby
{
    public partial class SaveLoadDialog : Form
    {
        public enum DialogMode
        {
            Save,
            Load
        }

        private DialogMode Mode { get; }
        public string SaveName { get; private set; }
        public GameTable.GameData Data { get; private set; }

        public SaveLoadDialog(DialogMode mode)
        {
            InitializeComponent();
            string[] texts = MainForm.GetMainForm.Settings.Localization[LocalizationKeys.SaveLoadForm].Split('_');
            Mode = mode;
            NameColumn.Text = texts[5];
            DateColumn.Text = texts[6];
            CnlButton.Text = texts[7];
            var saves = Directory.GetFiles(MainForm.ExecutablePath, "*.gsf").Select(F => F.Remove(F.Length - 4, 4).Split('\\').Last());
            SaveFilesView.Items.AddRange(saves.Select(F => new ListViewItem(F.Split('@'))).ToArray());
            switch (Mode)
            {
                case DialogMode.Save:
                    SaveNameBox.Text = texts[4];
                    Text = texts[0];
                    SaveLoadButton.Text = texts[2];
                    break;
                case DialogMode.Load:
                    SaveNameBox.Enabled = false;
                    Text = texts[1];
                    SaveLoadButton.Text = texts[3];
                    break;
            }
        }

        private void SaveFilesView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Mode == DialogMode.Save && SaveFilesView.SelectedIndices.Count > 0)
                SaveNameBox.Text = SaveFilesView.SelectedItems[0].SubItems[0].Text;
        }
        private void SaveFilesView_ItemActivate(object sender, EventArgs e)
        {
            if (Mode == DialogMode.Load)
                SaveLoadButton_Click(sender, e);
        }

        private void SaveLoadButton_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case DialogMode.Save:
                    SaveName = SaveNameBox.Text + '@' + DateTime.Now.ToString("dd.MMM.yyyy - HH.mm");
                    Data = (Owner.Owner as GameForm).Game.GetGameData((Owner.Owner as GameForm).Turn.TurnPlayerName);
                    File.WriteAllText($"{MainForm.ExecutablePath}\\{SaveName}.gsf", Data.ToString());
                    break;
                case DialogMode.Load:
                    SaveName = (from ListViewItem.ListViewSubItem item in SaveFilesView.SelectedItems[0].SubItems
                                select item.Text).Aggregate((S0, S1) => S0 + '@' + S1);
                    Data = new GameTable.GameData(File.ReadAllText($"{MainForm.ExecutablePath}\\{SaveName}.gsf"));
                    break;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        private void CnlButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
