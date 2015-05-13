using System;
using System.Text;

namespace BMonitor.Common.Models
{
    public class Range
    {
        public double Low { get; private set; }
        public double High { get; private set; }
        public bool Percent { get; private set; }
        public bool MatchInside { get; private set; }

        public double Limit // maybe this isnt realy a thing, but i dont really use the ranges so i dont want to test it yet
        {
            get { return (MatchInside) ? High : Low; }
        }

        public Range(double low, double high, bool percent = false, bool inside = false)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException("High must be greater than low.");

            Low = low;
            High = high;
            Percent = percent;
            MatchInside = inside;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            if (double.IsInfinity(Low))
            {
                sb.Append("~:");
                if (double.IsInfinity(High))
                {
                    return sb.ToString();
                }
                else
                {
                    sb.Append(High);
                }
            }
            else if (Low == 0d)
            {
                sb.Append(Low);
            } 
            else
            {
                if (double.IsInfinity(High))
                {
                    sb.Append(string.Format("{0}:", Low));
                }
                else
                {
                    sb.Append(string.Format("{0}:{1}", Low, High));
                }
            }
            return sb.ToString();
        }
    }

    public static class RangeUtil
    {
        public static bool Contains(this Range range, double value)
        {
            if (value < range.Low && range.Low != double.NegativeInfinity)
                return (range.MatchInside == false); // MatchInside == false means check that the value is outside

            if (value > range.High && range.High != double.PositiveInfinity)
                return (range.MatchInside == false);

            return range.MatchInside == true;
        }
    }
}
