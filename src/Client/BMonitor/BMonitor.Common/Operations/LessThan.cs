namespace BMonitor.Common.Operations
{
    public class LessThan : EvaluationOperation
    {
        public override string LongString { get { return "Less than"; } }
        public override string ShortString { get { return "<"; } }

        public override bool LimitBroken(double limit, double actual)
        {
            return actual < limit;
        }
    }
}