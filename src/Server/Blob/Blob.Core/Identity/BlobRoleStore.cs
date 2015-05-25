using System;
using System.Data.Entity;
using Blob.Core.Models;

namespace Blob.Core.Identity
{
    public class BlobRoleStore : GenericRoleStore<Role, Guid, BlobUserRole>
    {
        public BlobRoleStore(DbContext context) : base(context) { }
    }
}