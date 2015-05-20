using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blob.Proxies;
using Microsoft.Owin.Security;

namespace Before.Infrastructure.Identity
{
    public class BeforeUserManager : IdentityManagerClient
    {
        private BeforeUserManager(string endpointName, string username, string password)
            : base(endpointName, username, password) { }

        public static BeforeUserManager Create(string endpointName)
        {
            ClaimsPrincipal principal = ClaimsPrincipal.Current;
            string username = "customerUser1";
            string password = "password";
            return new BeforeUserManager(endpointName, username, password);
        }

        public async Task<IList<RoleListItemVm>> GetAllRolesAsync()
        {
            return null;
        }

        public async Task<IList<GroupListItemVm>> GetAllGroupsAsync()
        {
            return null;
        }

        public async Task CreateGroupAsync(CreateGroupDto dto)
        {

        }

        public async Task DeleteGroupAsync(DeleteGroupDto dto)
        {

        }

        public async Task UpdateGroupAsync(UpdateGroupDto dto)
        {

        }

        public async Task CreateRoleAsync(CreateRoleDto dto)
        {

        }

        public async Task DeleteRoleAsync(DeleteRoleDto dto)
        {

        }

        public async Task UpdateRoleAsync(UpdateRoleDto dto)
        {

        }

        public Task<GroupCreateVm> GetGroupCreateVmAsync(Guid groupId)
        {
            return null;
        }

        public Task<GroupDeleteVm> GetGroupDeleteVmAsync(Guid groupId)
        {
            return null;
        }

        public Task<GroupUpdateVm> GetGroupUpdateVmAsync(Guid groupId)
        {
            return null;
        }

        public Task<RoleCreateVm> GetRoleCreateVmAsync(Guid roleId)
        {
            return null;
        }

        public Task<RoleDeleteVm> GetRoleDeleteVmAsync(Guid roleId)
        {
            return null;
        }

        public Task<RoleUpdateVm> GetRoleUpdateVmAsync(Guid roleId)
        {
            return null;
        }
    }
}