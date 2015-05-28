using System;

namespace BMonitor.Common.Models.Metrics
{
    // http://dotnetcodr.com/2015/05/25/solid-principles-in-net-revisited-part-10-concentrating-on-enumerations-resolved/
    public abstract class Metric
    {
        protected Metric(string description)
        {
            if (string.IsNullOrEmpty(description)) 
                throw new ArgumentNullException("description");

            Description = description;
        }

        public string Description { get; protected set;}

        public abstract double ExtractActualValueFrom(PerformanceSummary performanceSummary);
    }
}
