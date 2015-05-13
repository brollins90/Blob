
namespace BMonitor.Common.Models
{
    public class PerformanceData
    {
        public string Critical { get; set; }
        public string Label { get; set; }
        public string Max { get; set; }
        public string Min { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Value { get; set; }
        public string Warning { get; set; }

        public override string ToString()
        {
            // 'label'=value[UOM];[warn];[crit];[min];[max]
            return string.Format("'{0}'={1}{2};{3};{4};{5};{6}", Label, Value, UnitOfMeasure, Warning, Critical, Min, Max);
        }
    }
}
