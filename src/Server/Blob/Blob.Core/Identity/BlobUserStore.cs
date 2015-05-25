using System;
using System.Data.Entity;
using Blob.Core.Models;

namespace Blob.Core.Identity
{
    public class BlobUserStore : GenericUserStore<User, Role, Guid, BlobUserLogin, BlobUserRole, BlobUserClaim>
    {
        public BlobUserStore(DbContext context) : base(context) { }
    }
}