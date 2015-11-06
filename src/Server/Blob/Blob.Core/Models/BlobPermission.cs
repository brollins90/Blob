using System;

namespace Blob.Core.Models
{
    public class BlobPermission
    {
        public Guid Id { get; set; }
        public string Operation { get; set; }
        public string Resource { get; set; }
    }
}
