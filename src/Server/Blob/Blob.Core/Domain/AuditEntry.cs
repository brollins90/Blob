
using System;

namespace Blob.Core.Domain
{
    public class AuditEntry
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public string Initiator { get; set; }
        public int AuditLevel { get; set; }
        public string Operation { get; set; }
        public string ResourceType { get; set; }
        public string Resource { get; set; }
    }
}
