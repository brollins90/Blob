using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMonitor.Common.Interfaces
{
    public class MonitorPerf
    {
        public string Critical { get; set; }
        public string Label { get; set; }
        public string Max { get; set; }
        public string Min { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string Value { get; set; }
        public string Warning { get; set; }

        public override string ToString()
        {
            // 'label'=value[UOM];[warn];[crit];[min];[max]
            return string.Format("'{0}'={1}{2};{3};{4};{5};{6}", Label, Value, UnitOfMeasure.ToString(), Warning, Critical, Min, Max);
        }
    }
}
