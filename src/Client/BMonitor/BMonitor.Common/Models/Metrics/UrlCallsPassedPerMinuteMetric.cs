
namespace BMonitor.Common.Models.Metrics
{
    public class UrlCallsPassedPerMinuteMetric : Metric
    {
        public UrlCallsPassedPerMinuteMetric()
            : base("Url calls passed per minute")
        { }

        public override double ExtractActualValueFrom(PerformanceSummary performanceSummary)
        {
            return performanceSummary.TotalPassedCallsPerMinute;
        }
    }
}
