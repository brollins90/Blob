namespace Blob.Core.Identity.Store
{
    using System;
    using System.Data.Entity;
    using Core.Models;

    public class BlobUserStore : GenericUserStore<User, Role, Guid, BlobUserLogin, BlobUserRole, BlobUserClaim>
    {
        public BlobUserStore(DbContext context) : base(context) { }
    }
}