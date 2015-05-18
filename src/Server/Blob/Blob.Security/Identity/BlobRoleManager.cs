using System;
using Blob.Core.Domain;
using log4net;
using Microsoft.AspNet.Identity;

namespace Blob.Security.Identity
{
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
