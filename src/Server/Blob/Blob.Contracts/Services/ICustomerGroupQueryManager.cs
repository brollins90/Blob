using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.Services
{
    [ServiceContract]
    public interface ICustomerGroupQueryManager
    {
        [OperationContract]
        Task<CustomerGroupCreateVm> GetCustomerGroupCreateVmAsync(Guid customerId);

        [OperationContract]
        Task<CustomerGroupDeleteVm> GetCustomerGroupDeleteVmAsync(Guid groupId);

        [OperationContract]
        Task<CustomerGroupSingleVm> GetCustomerGroupSingleVmAsync(Guid groupId);

        [OperationContract]
        Task<CustomerGroupUpdateVm> GetCustomerGroupUpdateVmAsync(Guid groupId);

        [OperationContract]
        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId);

        [OperationContract]
        Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId);

    }
}
