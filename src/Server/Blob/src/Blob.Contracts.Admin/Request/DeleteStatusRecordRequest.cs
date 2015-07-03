namespace Blob.Contracts.Request
{
    using System.Runtime.Serialization;

    [DataContract]
    public class DeleteStatusRecordRequest
    {
        [DataMember]
        public long RecordId { get; set; }
    }
}