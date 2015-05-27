using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.Services
{
    [ServiceContract]
    public interface ICustomerGroupCommandManager
    {
        [OperationContract]
        Task CreateCustomerGroupAsync(CreateCustomerGroupDto dto);

        [OperationContract]
        Task DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto);

        [OperationContract]
        Task UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto);

        [OperationContract]
        Task AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto);

        [OperationContract]
        Task AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto);

        [OperationContract]
        Task RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto);

        [OperationContract]
        Task RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto);
    }
}
