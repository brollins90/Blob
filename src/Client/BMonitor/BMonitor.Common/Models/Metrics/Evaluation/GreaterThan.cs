
namespace BMonitor.Common.Models.Metrics
{
    public class GreaterThan : EvaluationOperation
    {
        public GreaterThan() : base("Greater than") { }

        public override bool LimitBroken(double limit, double actual)
        {
            return actual > limit;
        }
    }
}
