using System;

namespace BMonitor.Common.Models
{
    public class UoM
    {
        public Type T { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }

        public UoM(string shortName, string longName, Type t)
        {
            ShortName = shortName;
            LongName = longName;
            T = t;
        }

        public virtual string GetString(dynamic value)
        {
            return value.ToString();
        }

        public static readonly UoM Byte = new UoM("B", "Byte", typeof(long));
        public static readonly UoM Percent = new UoM("%", "Percent", typeof(decimal));
    }
}
