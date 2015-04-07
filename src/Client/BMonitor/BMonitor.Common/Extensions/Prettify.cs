using System;

namespace BMonitor.Common.Extensions
{
    public static class Prettify
    {
        private const long ONE_KB = 1024;
        private const long ONE_MB = ONE_KB * 1024;
        private const long ONE_GB = ONE_MB * 1024;
        private const long ONE_TB = ONE_GB * 1024;


        public static double BytesToTb(this int value, int decimalPlaces = 0)
        {
            return ((long)value).BytesToTb(decimalPlaces);
        }

        public static double BytesToTb(this long value, int decimalPlaces = 0)
        {
            return Math.Round((double)value / ONE_TB, decimalPlaces);
        }

        public static double BytesToGb(this int value, int decimalPlaces = 0)
        {
            return ((long)value).BytesToGb(decimalPlaces);
        }

        public static double BytesToGb(this long value, int decimalPlaces = 0)
        {
            return Math.Round((double)value / ONE_GB, decimalPlaces);
        }

        public static double BytesToMb(this int value, int decimalPlaces = 0)
        {
            return ((long)value).BytesToMb(decimalPlaces);
        }

        public static double BytesToMb(this long value, int decimalPlaces = 0)
        {
            return Math.Round((double)value / ONE_MB, decimalPlaces);
        }

        public static double BytesToKb(this int value, int decimalPlaces = 0)
        {
            return ((long)value).BytesToKb(decimalPlaces);
        }

        public static double BytesToKb(this long value, int decimalPlaces = 0)
        {
            return Math.Round((double)value / ONE_KB, decimalPlaces);
        }

        public static double BytesToB(this int value, int decimalPlaces = 0)
        {
            return ((long)value).BytesToB(decimalPlaces);
        }

        public static double BytesToB(this long value, int decimalPlaces = 0)
        {
            return Math.Round((double) value, decimalPlaces);
        }

        public static string ToPrettySize(this int value, int decimalPlaces = 0)
        {
            return ((long)value).ToPrettySize(decimalPlaces);
        }

        public static string ToPrettySize(this long value, int decimalPlaces = 0)
        {
            double asTb = value.BytesToTb(decimalPlaces);
            double asGb = value.BytesToGb(decimalPlaces);
            double asMb = value.BytesToMb(decimalPlaces);
            double asKb = value.BytesToKb(decimalPlaces);
            double asB = value.BytesToB(decimalPlaces);

            string chosenValue = asTb > 1 ? string.Format("{0}Tb", asTb)
                : asGb > 1 ? string.Format("{0}Gb", asGb)
                : asMb > 1 ? string.Format("{0}Mb", asMb)
                : asKb > 1 ? string.Format("{0}Kb", asKb)
                : string.Format("{0}B", asB);
            return chosenValue;
        }

        public static string ToPrettyPercent(this double value)
        {
            return value.ToString("#0.0%");
        }
    }
}
