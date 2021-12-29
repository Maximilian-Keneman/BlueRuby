using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueRuby
{
    public static class HexConvert
    {
        public static string InHex(string Text)
        {
            if (Text == null)
                throw new ArgumentNullException(nameof(Text));
            return string.Join(" 0x", Encoding.Unicode.GetBytes(Text).Select(i => i.ToString("X2")));
        }
        public static string OutHex(string HexText)
        {
            string[] array = HexText.Split(' ');
            byte[] data = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
                if (array[i].StartsWith("0x"))
                    data[i] = Convert.ToByte(array[i].Substring(2), 16);
                else
                    data[i] = Convert.ToByte(array[i], 16);
            return Encoding.Unicode.GetString(data);
        }
    }

    public struct DoubleBool<T> where T : IComparable
    {
        private T Min;
        private bool IncludeMin;
        private T Max;
        private bool IncludeMax;

        public DoubleBool(T min, T max) : this(min, false, false, max) { }
        public DoubleBool(T min, bool includeMin, bool includeMax, T max)
        {
            Min = min;
            IncludeMin = includeMin;
            Max = max;
            IncludeMax = includeMax;
        }

        public bool GetBool(T value)
        {
           return (value.CompareTo(Min) > 0 || (IncludeMin && value.CompareTo(Min) == 0)) &&
                  (value.CompareTo(Max) < 0 || (IncludeMax && value.CompareTo(Max) == 0));
        }
        public static bool GetBool(T min, bool includeMin, T value, bool includeMax, T max)
        {
            return new DoubleBool<T>(min, includeMin, includeMax, max).GetBool(value);
        }

        public override string ToString()
        {
            return $"{Min} {(IncludeMin ? "<=" : "<")} X {(IncludeMax ? "<=" : "<")} {Max}";
        }
        public string ToString(T value)
        {
            return $"{Min} {(IncludeMin ? "<=" : "<")} {value} {(IncludeMax ? "<=" : "<")} {Max}";
        }
    } 

    public class Timer
    {
        private bool Hour;
        private bool Min;
        private bool Sec;
        private bool MSec;
        /// <summary>
        /// Определяет тип класса. Секундомер если <see langword="true"/>, таймер если <see langword="false"/>.
        /// </summary>
        public bool TimerUp { get; }
        public int TimerDown { get; private set; }
        private bool TimeGo = false;
        private DateTime StartTime;
        public event EventHandler TimeOutEvent;

        /// <summary>
        /// Инициализирует секундомер.
        /// </summary>
        /// <param name="hour">Отображение часов.</param>
        /// <param name="min">Отображение минут.</param>
        /// <param name="sec">Отображение секунд.</param>
        /// <param name="msec">Отображение миллисекунд.</param>
        public Timer(bool hour, bool min, bool sec, bool msec)
        {
            Hour = hour;
            Min = min;
            Sec = sec;
            MSec = msec;
            TimerUp = true;
            TimerDown = 0;
        }
        /// <summary>
        /// Инициализирует таймер.
        /// </summary>
        /// <param name="hour">Отображение часов.</param>
        /// <param name="min">Отображение минут.</param>
        /// <param name="sec">Отображение секунд.</param>
        /// <param name="msec">Отображение миллисекунд.</param>
        /// <param name="hourT">Количество часов.</param>
        /// <param name="minT">Количество минут.</param>
        /// <param name="secT">Количество секунд.</param>
        /// <param name="msecT">Количество миллисекунд.</param>
        public Timer(bool hour, bool min, bool sec, bool msec, int hourT, int minT, int secT, int msecT)
        {
            Hour = hour;
            Min = min;
            Sec = sec;
            MSec = msec;
            TimerUp = false;
            TimerDown = msecT + 1000 * (secT + 60 * (minT + 60 * hourT));
        }

        public void Start()
        {
            StartTime = TimerUp ? DateTime.UtcNow - ToTime() : DateTime.UtcNow + ToTime();
            TimeGo = true;
        }
        public void Stop()
        {
            TimeGo = false;
        }
        public void Tick()
        {
            if (TimeGo)
            {
                if (TimerUp)
                    TimerDown = (int)(DateTime.UtcNow - StartTime).TotalMilliseconds;
                else
                    TimerDown = (int)(StartTime - DateTime.UtcNow).TotalMilliseconds;
                if (TimerDown < 0)
                    TimeOutEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public TimeSpan ToTime()
        {
            int msec = TimerDown % 1000;
            int sec = (TimerDown / 1000) % 60;
            int min = ((TimerDown / 1000) / 60) % 60;
            int hour = ((TimerDown / 1000) / 60) / 60;
            return new TimeSpan(hour / 24, hour % 24, min, sec, msec);
        }
        public string ToString(char split)
        {
            List<string> s = new List<string>();
            int msec = TimerDown % 1000;
            int sec = (TimerDown / 1000) % 60;
            int min = ((TimerDown / 1000) / 60) % 60;
            int hour = ((TimerDown / 1000) / 60) / 60;
            if (Hour)
                s.Add(hour.ToString());
            else
                min += hour * 60;
            if (Min)
                s.Add(min.ToString("00"));
            else
                sec += min * 60;
            if (Sec)
                s.Add(sec.ToString("00"));
            else
                msec += sec * 1000;
            if (MSec)
                s.Add(msec.ToString("000"));
            if (s.Count == 0)
                throw new Exception("Неопределены отоброжаемые элементы");
            else
                return string.Join(split.ToString(), s);
        }
    }

    [Serializable]
    public class NumberException : ArgumentException
    {
        public NumberException() { }
        public NumberException(string message) : base(message) { }
        public NumberException(string message, Exception inner) : base(message, inner) { }
        public NumberException(string paramName, int count) : base("Параметр отличается от допустимого значения " + count, paramName) { }
        public NumberException(string paramName, int min, int max) : base("Параметр вне диапазона [" + min + ", " + max + "]", paramName) { }
        protected NumberException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
