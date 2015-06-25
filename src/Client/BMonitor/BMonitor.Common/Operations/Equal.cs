using System;

namespace BMonitor.Common.Operations
{
    public class Equal : EvaluationOperation
    {
        public override string LongString { get { return "Equal"; } }
        public override string ShortString { get { return "=="; } }

        public override bool LimitBroken(double limit, double actual)
        {
            return Math.Abs(actual - limit) < .01;
        }
    }
}