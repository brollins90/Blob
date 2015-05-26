using System;
using Blob.Core.Identity;
using Blob.Core.Identity.Models;

namespace Blob.Core.Models
{
    public class User : GenericUser<Guid, BlobUserLogin, BlobUserRole, BlobUserClaim>
    {
        public DateTime CreateDateUtc { get; set; }
        public bool Enabled { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        //public Guid CustomerId
        //{
        //    get { return Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f"); }
        //}
    }
}
