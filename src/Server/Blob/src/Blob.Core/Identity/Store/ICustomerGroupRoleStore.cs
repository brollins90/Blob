//namespace Blob.Core.Identity.Store
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Threading.Tasks;
//    using Core.Models;

//    public interface ICustomerGroupRoleStore
//    {
//        Task AddRoleToGroupAsync(CustomerGroup group, string roleName);
//        Task RemoveRoleFromGroupAsync(CustomerGroup group, string roleName);
//        Task<IList<Role>> GetRolesForGroupAsync(Guid groupId);
//        Task<bool> HasRoleAsync(CustomerGroup group, string roleName);
//    }
//}