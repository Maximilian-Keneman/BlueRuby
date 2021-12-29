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
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }
        public new void Show(IWin32Window owner)
        {
            base.Show(owner);
            DebugGame game = (owner as GameForm).Game as DebugGame;
            checkBox1.Checked = game.Start;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            ((Owner as GameForm).Game as DebugGame).ChangeStart(checkBox1.Checked);
        }

        public enum DebugMode
        {
            Empty,
            TblLoaction,
            BotMaps,
            Ship
        }
        public DebugMode Mode;
        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                Mode = DebugMode.TblLoaction;
        }
        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                Mode = DebugMode.BotMaps;
                comboBox1.Enabled = true;
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedIndex = -1;
            }
        }
        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                Mode = DebugMode.Empty;
        }
        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                Mode = DebugMode.Ship;
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
                BotMapsMode = comboBox1.Text;
            else
                BotMapsMode = "";
        }
        public string BotMapsMode { get; private set; } = "";

    }

    public class DebugGame : GameTable
    {
        private Point sectorLocation = new Point(-1, -1);
        private bool selectChanged = false;
        private GameMap map;

        public DebugGame(GameTable game) : base(game)
        {
            map = GameMap.ClearMap(Size);
        }

        public void StartGame()
        {
            ChangeTimer.Change(0, 10);
        }
        public Point SectorLocation
        {
            get => sectorLocation;
            set
            {
                sectorLocation = value;
                selectChanged = true;
            }
        }
        public Image Show(DebugForm.DebugMode mode, object state)
        {
            Image Game = Show();
            if (selectChanged)
            {
                selectChanged = false;
                switch (mode)
                {
                    case DebugForm.DebugMode.TblLoaction:
                    {
                        int[,] newMap = new int[Size.Width, Size.Height];
                        for (int x = 0; x < Size.Width; x++)
                            for (int y = 0; y < Size.Height; y++)
                                newMap[x, y] = x * 100 + y; // $"{Sectors[i, j].TblX}, {Sectors[i, j].TblY}"
                    }
                    break;
                    case DebugForm.DebugMode.BotMaps:
                        if (SectorLocation != new Point(-1, -1))
                        {
                            Sector GoalSector = this[SectorLocation];
                            WorldParts.BotMaps.Keys? key = GoalSector.Type switch
                            {
                                Sector.TypeSector.FinishLight => WorldParts.BotMaps.Keys.FinishLight,
                                Sector.TypeSector.FinishDark => WorldParts.BotMaps.Keys.FinishDark,
                                Sector.TypeSector.Fortress => WorldParts.BotMaps.Keys.Fortress,
                                Sector.TypeSector.MagicTower => WorldParts.BotMaps.Keys.MagicTower,
                                Sector.TypeSector.Catacomb => WorldParts.BotMaps.Keys.Catacomb,
                                _ => null
                            };
                            if (key != null)
                            {
                                (bool dragon, bool gold) = (state as string) switch
                                {
                                    "Standart" => (false, false),
                                    "Dragon" => (true, false),
                                    "Gold" => (false, true),
                                    _ => (true, true)
                                };
                                if (!dragon || !gold)
                                    map = Maps.GetMap(key.Value, SectorLocation, dragon, gold, new Point());
                            }
                        }
                        break;
                    case DebugForm.DebugMode.Ship:
                    {
                        if (SectorLocation != new Point(-1, -1) && this[SectorLocation].Type == Sector.TypeSector.Water)
                        {
                            int[][][] WaterTable = WorldParts.GetWaterTable(this[SectorLocation].TblLocation);
                            int[,] newMap = GameMap.ClearMap(Size);
                            for (int i1 = 0; i1 < WorldParts.WaterPaths.Length; i1++)
                                for (int i2 = 0; i2 < WorldParts.WaterPaths[i1].Length; i2++)
                                    for (int i3 = 0; i3 < WorldParts.WaterPaths[i1][i2].Length; i3++)
                                    {
                                        Point point = WorldParts.WaterPaths[i1][i2][i3];
                                        newMap[point.X, point.Y] = WaterTable[i1][i2] != null ? WaterTable[i1][i2][i3] : -1;
                                        map = new GameMap(newMap);
                                    }
                        }
                    }
                    break;
                    default:
                        map = GameMap.ClearMap(Size);
                        break;
                }
            }
            using (Graphics g = Graphics.FromImage(Game))
            {
                for (int i = 0; i < SectorsSize.Width; i++)
                    for (int j = 0; j < SectorsSize.Height; j++)
                        if (map[i, j] > -1)
                            g.DrawString(map[i, j].ToString(), SystemFonts.DefaultFont, Brushes.Black, Sectors[i, j].ImgLocation);
            }
            return Game;
        }

        public void ChangeStart(bool value)
            => Start = value;
    }
}
