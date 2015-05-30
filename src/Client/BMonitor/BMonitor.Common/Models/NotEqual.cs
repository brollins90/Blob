using System;

namespace BMonitor.Common.Models
{
    public class NotEqual : EvaluationOperation
    {
        public override string LongString { get { return "NotEqual"; } }
        public override string ShortString { get { return "!="; } }

        public override bool LimitBroken(double limit, double actual)
        {
            return Math.Abs(actual - limit) > .01;
        }
    }
}