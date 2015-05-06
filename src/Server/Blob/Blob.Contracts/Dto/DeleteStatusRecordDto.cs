using System.Runtime.Serialization;

namespace Blob.Contracts.Dto
{
    [DataContract]
    public class DeleteStatusRecordDto
    {
        [DataMember]
        public long RecordId { get; set; }
    }
}
