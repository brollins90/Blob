using Blob.Domain.Identity;
using System;

namespace Blob.Core.Domain
{
    public class BlobUserClaim : GenericUserClaim<Guid> { }
    public class BlobUserLogin : GenericUserLogin<Guid> { }
    public class BlobUserRole : GenericUserRole<Guid> { }

    public class User : GenericUser<Guid, BlobUserLogin, BlobUserRole, BlobUserClaim>
    {
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
