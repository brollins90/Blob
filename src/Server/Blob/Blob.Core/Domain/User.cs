using Blob.Core.Identity;
using System;

namespace Blob.Core.Domain
{
    public class BlobUserClaim : GenericUserClaim<Guid> { }
    public class BlobUserLogin : GenericUserLogin<Guid> { }
    public class BlobUserRole : GenericUserRole<Guid> { }

    public class User : GenericUser<Guid, BlobUserLogin, BlobUserRole, BlobUserClaim>
    {
        public virtual Customer Customer { get; set; }
    }
}
