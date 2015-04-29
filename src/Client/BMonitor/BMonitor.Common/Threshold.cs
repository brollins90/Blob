using System.Text;

namespace BMonitor.Common
{
    public class Threshold
    {
        public Range Critical { get; private set; }
        public Range Warning { get; private set; }

        public Threshold(Range critical, Range warning)
        {
            Critical = critical;
            Warning = warning;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Warning != null)
            {
                sb.Append(string.Format("Warning: start=%g end=%g; ", Warning.Low, Warning.High));
                sb.Append(string.Format("(%d:%d) ", double.IsInfinity(Warning.Low), double.IsInfinity(Warning.High)));
            }
            else
            {
                sb.Append("Warning not set; ");
            }
            if (Critical != null)
            {
                sb.Append(string.Format("Critical: start=%g end=%g; ", Critical.Low, Critical.High));
                sb.Append(string.Format("(%d:%d) ", double.IsInfinity(Critical.Low), double.IsInfinity(Critical.High)));
            }
            else
            {
                sb.Append("Critical not set; ");
            }
            sb.Append("\n");

            return sb.ToString();
        }
    }

    public static class ThresholdUtil
    {

        public static AlertLevel CheckAlertLevel(this Threshold threshold, double value)
        {
            if (threshold.Critical == null && threshold.Warning == null)
                return AlertLevel.OK;

            if (threshold.Critical != null)
            {
                if (threshold.Critical.Contains(value))
                {
                    return AlertLevel.CRITICAL;
                }
            }
            if (threshold.Warning != null)
            {
                if (threshold.Warning.Contains(value))
                {
                    return AlertLevel.WARNING;
                }
            }
            return AlertLevel.OK;
        }
    }
}
