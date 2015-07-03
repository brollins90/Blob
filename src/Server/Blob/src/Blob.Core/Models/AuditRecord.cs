namespace Blob.Core.Models
{
    using System;

    public class AuditRecord
    {
        public long Id { get; set; }
        public DateTime RecordTimeUtc { get; set; }
        public string Initiator { get; set; }
        public int AuditLevel { get; set; }
        public string Operation { get; set; }
        public string ResourceType { get; set; }
        public string Resource { get; set; }
    }
}