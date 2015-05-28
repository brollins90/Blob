
using System;

namespace BMonitor.Common.Models.Metrics
{
    public abstract class EvaluationOperation
    {
        private string _description;

        public EvaluationOperation(string description)
        {
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException("Description");
            _description = description;
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public abstract bool LimitBroken(double limit, double actual);
    }
}
