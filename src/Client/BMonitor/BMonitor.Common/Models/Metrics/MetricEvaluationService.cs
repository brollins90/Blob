//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BMonitor.Common.Models.Metric
//{
//    public class MetricEvaluationService
//    {
//        public ThresholdEvaluationResult EvaluateThreshold(PerformanceSummary performanceSummary, Threshold threshold)
//        {
//            ThresholdEvaluationResult thresholdEvaluationResult = new ThresholdEvaluationResult();
//            double thresholdValue = threshold.Limit;
//            if (threshold.PerformanceMetricType == PerformanceMetricType.AverageResponseTime)
//            {
//                double averageResponseTimePerPage = performanceSummary.AverageResponseTimePerPage;
//                switch (threshold.EvaluationOperator)
//                {
//                    case EvaluationOperator.GreaterThan:
//                        if (thresholdValue < averageResponseTimePerPage)
//                        {
//                            thresholdEvaluationResult.ThresholdBroken = true;
//                        }
//                        break;
//                    case EvaluationOperator.LessThan:
//                        if (thresholdValue > averageResponseTimePerPage)
//                        {
//                            thresholdEvaluationResult.ThresholdBroken = true;
//                        }
//                        break;
//                }
//            }

//            if (threshold.PerformanceMetricType == PerformanceMetricType.UrlCallsPassedPerMinute)
//            {
//                double urlCallsPassedPerMinute = performanceSummary.TotalPassedCallsPerMinute;
//                switch (threshold.EvaluationOperator)
//                {
//                    case EvaluationOperator.GreaterThan:
//                        if (thresholdValue < urlCallsPassedPerMinute)
//                        {
//                            thresholdEvaluationResult.ThresholdBroken = true;
//                        }
//                        break;
//                    case EvaluationOperator.LessThan:
//                        if (thresholdValue > urlCallsPassedPerMinute)
//                        {
//                            thresholdEvaluationResult.ThresholdBroken = true;
//                        }
//                        break;
//                }
//            }

//            return thresholdEvaluationResult;
//        }
//    }
//}
