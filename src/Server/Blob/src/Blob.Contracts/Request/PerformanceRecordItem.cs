namespace Blob.Contracts.Request
{
    using System.Runtime.Serialization;

    [DataContract]
    public class PerformanceRecordItem
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
    }
}