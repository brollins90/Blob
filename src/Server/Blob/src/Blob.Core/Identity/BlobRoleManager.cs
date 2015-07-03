namespace Blob.Core.Identity
{
    using System;
    using Core.Models;
    using log4net;
    using Microsoft.AspNet.Identity;

    public interface IRoleManagerService { }

    public class BlobRoleManager : RoleManager<Role, Guid>, IRoleManagerService
    {
        private readonly ILog _log;

        public BlobRoleManager(IRoleStore<Role, Guid> store, ILog log) : this(store)
        {
            _log = log;
        }

        public BlobRoleManager(IRoleStore<Role, Guid> store) : base(store)
        {

        }
    }
}