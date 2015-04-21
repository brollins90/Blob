using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class PerformanceDataValue
    {
        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string UnitOfMeasure { get; set; }

        [DataMember]
        public string Warning { get; set; }

        [DataMember]
        public string Critical { get; set; }

        [DataMember]
        public string Min { get; set; }

        [DataMember]
        public string Max { get; set; }

        public override string ToString()
        {
            return string.Format("PerformanceDataValue("
                                 + "Label: " + Label
                                 + ", Value: " + Value
                                 + ", UnitOfMeasure: " + UnitOfMeasure
                                 + ", Warning: " + Warning
                                 + ", Critical: " + Critical
                                 + ", Min: " + Min
                                 + ", Max: " + Max
                                 + ")");
        }
    }
}
