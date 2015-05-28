
namespace BMonitor.Common.Models.Metrics
{
    public class AverageResponseTimeMetric : Metric
    {
        public AverageResponseTimeMetric()
            : base("Average response time per page")
        { }

        public override double ExtractActualValueFrom(PerformanceSummary performanceSummary)
        {
            return performanceSummary.AverageResponseTimePerPage;
        }
    }
}
