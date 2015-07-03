namespace Blob.Contracts.Request
{
    using System.Runtime.Serialization;

    [DataContract]
    public class DeletePerformanceRecordRequest
    {
        [DataMember]
        public long RecordId { get; set; }
    }
}