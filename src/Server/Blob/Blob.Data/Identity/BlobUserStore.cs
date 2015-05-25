using System;
using System.Data.Entity;
using Blob.Core.Models;

namespace Blob.Data.Identity
{
    public class BlobUserStore : GenericUserStore<User, Role, Guid, BlobUserLogin, BlobUserRole, BlobUserClaim>
    {
        public BlobUserStore(DbContext context) : base(context) { }
    }
}