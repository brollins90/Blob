namespace BMonitor.Common.Models
{
    public class GreaterThan : EvaluationOperation
    {
        public override string LongString { get { return "Greater than"; } }
        public override string ShortString { get { return ">"; } }

        public override bool LimitBroken(double limit, double actual)
        {
            return actual > limit;
        }
    }
}