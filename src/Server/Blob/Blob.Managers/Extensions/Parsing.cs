
namespace Blob.Managers.Extensions
{
    public static class Parsing
    {
        public static decimal ToDecimal(this string s)
        {
            return decimal.Parse(s);
        }

        public static decimal? ToNullableDecimal(this string s)
        {
            decimal temp;
            // replace null with default
            decimal? numericValue =
              decimal.TryParse(s, out temp) ? temp : default(decimal?);
            return numericValue;
        }
    }
}
