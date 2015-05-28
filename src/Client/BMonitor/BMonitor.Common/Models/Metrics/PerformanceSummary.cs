
namespace BMonitor.Common.Models.Metrics
{
    public class PerformanceSummary
    {
        public double TotalPassedLoops { get; set; }
        public double TotalFailedLoops { get; set; }
        public double TotalPassedCallsPerMinute { get; set; }
        public double AverageNetworkThroughput { get; set; }
        public double AverageSessionTimePerLoop { get; set; }
        public double AverageResponseTimePerLoop { get; set; }
        public double WebTransactionRate { get; set; }
        public double AverageResponseTimePerPage { get; set; }
        public double TotalHttpCalls { get; set; }
        public double AverageNetworkConnectTime { get; set; }
        public double TotalTransmittedBytes { get; set; }
    }
}
