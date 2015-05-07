using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Blob;
using Blob.Contracts.Dto;

namespace Blob.Services.Before
{
    [ServiceBehavior]
    //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
    public class BeforeCommandService : IBlobCommandManager
    {
        private readonly IBlobCommandManager _blobCommandManager;

        public BeforeCommandService(IBlobCommandManager blobCommandManager)
        {
            _blobCommandManager = blobCommandManager;
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task DisableCustomerAsync(DisableCustomerDto dto)
        {
            await _blobCommandManager.DisableCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task EnableCustomerAsync(EnableCustomerDto dto)
        {
            await _blobCommandManager.EnableCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            await _blobCommandManager.UpdateCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            await _blobCommandManager.IssueCommandAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task DisableDeviceAsync(DisableDeviceDto dto)
        {
            await _blobCommandManager.DisableDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task EnableDeviceAsync(EnableDeviceDto dto)
        {
            await _blobCommandManager.EnableDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto)
        {
            return await _blobCommandManager.RegisterDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            await _blobCommandManager.UpdateDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            await _blobCommandManager.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            await _blobCommandManager.DeletePerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            await _blobCommandManager.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            await _blobCommandManager.DeleteStatusRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task DisableUserAsync(DisableUserDto dto)
        {
            await _blobCommandManager.DisableUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task EnableUserAsync(EnableUserDto dto)
        {
            await _blobCommandManager.EnableUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task UpdateUserAsync(UpdateUserDto dto)
        {
            await _blobCommandManager.UpdateUserAsync(dto).ConfigureAwait(false);
        }
    }
}
