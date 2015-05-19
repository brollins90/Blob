﻿using System;
using Blob.Identity;

namespace Blob.Core.Domain
{
    public class BlobUserClaim : GenericUserClaim<Guid> { }
    public class BlobUserLogin : GenericUserLogin<Guid> { }
    public class BlobUserRole : GenericUserRole<Guid> { }

    public class User : GenericUser<Guid, BlobUserLogin, BlobUserRole, BlobUserClaim, BlobUserGroup>
    {
        public DateTime CreateDate { get; set; }
        public bool Enabled { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
