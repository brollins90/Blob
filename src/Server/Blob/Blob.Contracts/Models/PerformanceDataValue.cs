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
            return string.Format("PerformanceDataValue:\n"
                + "Label: {0}\n"
                + "Value: {1}\n"
                + "UnitOfMeasure: {2}\n"
                + "Warning: {3}\n"
                + "Critical: {4}\n"
                + "Min: {5}\n"
                + "Max: {6}\n",
                                 Label,
                                 Value,
                                 UnitOfMeasure,
                                 Warning,
                                 Critical,
                                 Min,
                                 Max);
        }
    }
}
