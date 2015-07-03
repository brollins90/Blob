namespace Blob.Core.Models
{
    using System;

    public class BlobPermission
    {
        public Guid Id { get; set; }
        public string Operation { get; set; }
        public string Resource { get; set; }
    }
}