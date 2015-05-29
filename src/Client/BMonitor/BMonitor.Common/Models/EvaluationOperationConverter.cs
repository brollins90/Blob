using System.ComponentModel;
using System.Globalization;

namespace BMonitor.Common.Models
{
    public class EvaluationOperationConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string lowerInvariant = (value as string).ToLowerInvariant();
                if (lowerInvariant.Equals("greaterthan"))
                {
                    return EvaluationOperation.GreaterThan;
                }
                else if (lowerInvariant.Equals("lessthan"))
                    {
                        return EvaluationOperation.LessThan;
                    }
            }

            return EvaluationOperation.LessThan;
        }
    }
}
