using System;

namespace BMonitor.Common.Extensions
{
    // http://stackoverflow.com/a/14489026/148256
    public static class Prettify
    {
        private const long ONE_KB = 1024;
        private const long ONE_MB = ONE_KB * 1024;
        private const long ONE_GB = ONE_MB * 1024;
        private const long ONE_TB = ONE_GB * 1024;

        public static string ToPrettySize(this int value, int decimalPlaces = 0)
        {
            return ((long)value).ToPrettySize(decimalPlaces);
        }

        public static string ToPrettySize(this long value, int decimalPlaces = 0)
        {
            double asTb = Math.Round((double)value / ONE_TB, decimalPlaces);
            double asGb = Math.Round((double)value / ONE_GB, decimalPlaces);
            double asMb = Math.Round((double)value / ONE_MB, decimalPlaces);
            double asKb = Math.Round((double)value / ONE_KB, decimalPlaces);

            string chosenValue = asTb > 1 ? string.Format("{0}Tb", asTb)
                : asGb > 1 ? string.Format("{0}Gb", asGb)
                : asMb > 1 ? string.Format("{0}Mb", asMb)
                : asKb > 1 ? string.Format("{0}Kb", asKb)
                : string.Format("{0}B", Math.Round((double)value, decimalPlaces));
            return chosenValue;
        }

        public static string ToPrettyPercent(this double value)
        {
            return value.ToString("#0.0%");
        }
    }
}
