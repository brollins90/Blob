namespace BMonitor.Common.Models
{
    public class LessThan : EvaluationOperation
    {
        public LessThan() : base("Less than") { }

        public override bool LimitBroken(double limit, double actual)
        {
            return actual < limit;
        }
    }
}