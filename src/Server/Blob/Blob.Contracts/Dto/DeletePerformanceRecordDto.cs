using System.Runtime.Serialization;

namespace Blob.Contracts.Dto
{
    [DataContract]
    public class DeletePerformanceRecordDto
    {
        [DataMember]
        public long RecordId { get; set; }
    }
}
