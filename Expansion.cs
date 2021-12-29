using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace BlueRuby
{
    static class Images
    {
        public static Image SetRound(Color color, float radius)
        {
            float d = radius * 2;
            Image img = new Bitmap(Convert.ToInt32(Math.Round(d, MidpointRounding.AwayFromZero)), Convert.ToInt32(Math.Round(d, MidpointRounding.AwayFromZero)));
            using Graphics g = Graphics.FromImage(img);
            g.FillEllipse(new SolidBrush(color), 0, 0, d, d);
            return img;
        }
        public static Image SetSquare(Color color, Size size)
        {
            Image img = new Bitmap(size.Width, size.Height);
            using Graphics g = Graphics.FromImage(img);
            g.Clear(color);
            return img;
        }
        public static Image SetDeck(Image[] deck, Size ImageSize, Size delta)
        {
            Image img = new Bitmap(ImageSize.Width, ImageSize.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.Clear(Color.Transparent);
                for (int i = 0; i < deck.Length; i++)
                    g.DrawImage(new Bitmap(deck[i], Card.CardSize), new Point(i * delta.Width, i * delta.Height));
            }
            return img;
        }
        public static Image SetTransparentImage(Size ImageSize)
        {
            Image img = new Bitmap(ImageSize.Width, ImageSize.Height);
            using (Graphics g = Graphics.FromImage(img))
                g.Clear(Color.Transparent);
            return img;
        }
        public static Image GetFragment(Image source, Size fragmentSize, Rectangle sourceRect)
        {
            Image img = new Bitmap(fragmentSize.Width, fragmentSize.Height);
            using (Graphics g = Graphics.FromImage(img))
                g.DrawImage(source, 0, 0, sourceRect, GraphicsUnit.Pixel);
            return img;
        }
        public static Image GetFragment(Image source, Size fragmentSize, Rectangle sourceRect, Size needSize)
            => new Bitmap(GetFragment(source, fragmentSize, sourceRect), needSize);
    }

    public class Sound
    {
        private MediaPlayer m_mediaPlayer;

        public Sound(string filename)
        {
            m_mediaPlayer = new MediaPlayer();
            m_mediaPlayer.Open(new Uri(filename));
        }

        public void Play() => m_mediaPlayer.Play();
        public void Play(int volume)
        {
            Volume = volume;
            Play();
        }
        public void Stop() => m_mediaPlayer.Stop();

        // `volume` is assumed to be between 0 and 100.
        // MediaPlayer volume is a float value between 0 and 1.
        public int Volume
        {
            get => (int)(m_mediaPlayer.Volume * 100);
            set => m_mediaPlayer.Volume = value / 100f;
        }

        public double Speed
        {
            get => m_mediaPlayer.SpeedRatio;
            set => m_mediaPlayer.SpeedRatio = value;
        }
    }

    static class Expansion
    {
        public static Random Rnd = new Random();
        public static (int g1, int g2) GoDir(this (Point p1, Point p2) points)
        {
            int dX = points.p1.X - points.p2.X;
            int dY = points.p1.Y - points.p2.Y;
            if (dX == 0)
            {
                return dY switch
                {
                    1 => (3, 1),
                    -1 => (1, 3)
                };
            }
            else if (dY == 0)
            {
                return dX switch
                {
                    1 => (0, 2),
                    -1 => (2, 0)
                };
            }
            return (-1, -1);
        }
        public static Point ToPoint(this (int x, int y) p) => new Point(p.x, p.y);
        public static Point ToPoint(this PointF p) => new Point((int)p.X, (int)p.Y);
        public static int StepCount(this Point p1, Point p2) => Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        public static Size ToSize(this Point p) => new Size(p.X, p.Y);
        public static Size Multiple(this Size size, int delta) => new Size(size.Width * delta, size.Height * delta);
        public static Sector[] IncreasingArray(this Sector[] Arr, int count, bool UpOrDown)
        {
            Sector[] newArr = new Sector[Arr.Length + count];
            if (UpOrDown)
                for (int i = 0; i < Arr.Length; i++)
                    newArr[i] = Arr[i];
            else
                for (int i = Arr.Length - 1; i >= 0; i--)
                    newArr[i + count] = Arr[i];
            return newArr;
        }
        public static List<T> Shuffle<T>(this List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Rnd.Next(i);
                var t = list[i];
                list[i] = list[j];
                list[j] = t;
            }
            return list;
        }
        public static int IndexOfMin(this IEnumerable<int> collection)
            => collection.ToList().IndexOf(collection.Min());
        public static T MinElement<T>(this IEnumerable<T> collection, Func<T, int> comparer)
        {
            var values = collection.Select(comparer);
            int i = values.IndexOfMin();
            return collection.ToArray()[i];
        }
        public static Team Opponent(this Team myTeam)
        {
            if (myTeam == Team.Light)
                return Team.Dark;
            else if (myTeam == Team.Dark)
                return Team.Light;
            else
                return Team.Fantom;
        }
    }
}