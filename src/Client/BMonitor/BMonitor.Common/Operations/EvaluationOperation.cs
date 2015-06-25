using System.ComponentModel;

namespace BMonitor.Common.Operations
{
    [TypeConverter(typeof(EvaluationOperationConverter))]
    public abstract class EvaluationOperation
    {
        public static EvaluationOperation GreaterThan { get { return new GreaterThan(); } }
        public static EvaluationOperation LessThan { get { return new LessThan(); } }
        public static EvaluationOperation Equal { get { return new Equal(); } }
        public static EvaluationOperation NotEqual { get { return new NotEqual(); } }
        
        public abstract string LongString { get; }
        public abstract string ShortString { get; }
        public abstract bool LimitBroken(double limit, double actual);
    }
}
