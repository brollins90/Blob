namespace Blob.Core.Models
{
    using System;
    using Identity.Models;

    public class User : GenericUser<Guid, BlobUserLogin, BlobUserRole, BlobUserClaim>
    {
        public DateTime CreateDateUtc { get; set; }
        public bool Enabled { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}