using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class DeletePerformanceRecordDto
    {
        [DataMember]
        public long RecordId { get; set; }
    }
}
