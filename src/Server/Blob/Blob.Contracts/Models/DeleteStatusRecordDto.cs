using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class DeleteStatusRecordDto
    {
        [DataMember]
        public long RecordId { get; set; }
    }
}
