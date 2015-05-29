using System;
using System.ComponentModel;

namespace BMonitor.Common.Models
{
    [TypeConverter(typeof(EvaluationOperationConverter))]
    public abstract class EvaluationOperation
    {
        public static EvaluationOperation GreaterThan { get { return new GreaterThan(); } }
        public static EvaluationOperation LessThan { get { return new LessThan(); } }

        private readonly string _description;

        protected EvaluationOperation(string description)
        {
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException("description");
            _description = description;
        }

        public string Description { get { return _description; } }

        public abstract bool LimitBroken(double limit, double actual);
    }
}
