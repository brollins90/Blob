using System.ComponentModel;
using System.Globalization;

namespace BMonitor.Common.Operations
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
                else if (lowerInvariant.Equals("equal"))
                {
                    return EvaluationOperation.Equal;
                }
                else if (lowerInvariant.Equals("notequal"))
                {
                    return EvaluationOperation.NotEqual;
                }
            }

            return EvaluationOperation.LessThan;
        }
    }
}
