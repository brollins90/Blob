using System;
using System.ComponentModel;

namespace BMonitor.Common.Models
{
    [TypeConverter(typeof(EvaluationOperationConverter))]
    public abstract class EvaluationOperation
    {
        public static EvaluationOperation GreaterThan { get { return new GreaterThan(); } }
        public static EvaluationOperation LessThan { get { return new LessThan(); } }
        
        public abstract string LongString { get; }
        public abstract string ShortString { get; }
        public abstract bool LimitBroken(double limit, double actual);
    }
}
