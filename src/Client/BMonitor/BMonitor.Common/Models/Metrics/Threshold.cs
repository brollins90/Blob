using System;

namespace BMonitor.Common.Models.Metrics
{
    public class Threshold
    {
        private Metric _metric;
        private EvaluationOperation _operation;
        private double _thresholdValue;

        public Threshold(Metric metric, EvaluationOperation operation, double thresholdValue)
        {
            if (metric == null) throw new ArgumentNullException("Metric");
            if (operation == null) throw new ArgumentNullException("Operation");
            _metric = metric;
            _operation = operation;
            _thresholdValue = thresholdValue;
        }

        public ThresholdEvaluationResult Evaluate(PerformanceSummary performanceSummary)
        {
            ThresholdEvaluationResult res = new ThresholdEvaluationResult();
            double actualValue = _metric.ExtractActualValueFrom(performanceSummary);
            bool broken = _operation.LimitBroken(_thresholdValue, actualValue);
            res.ThresholdBroken = broken;
            return res;
        }
    }
}
