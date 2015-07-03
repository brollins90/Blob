namespace Blob.Core.Identity.Store
{
    using System;
    using System.Data.Entity;
    using Core.Models;

    public class BlobRoleStore : GenericRoleStore<Role, Guid, BlobUserRole>
    {
        public BlobRoleStore(DbContext context) : base(context) { }
    }
}