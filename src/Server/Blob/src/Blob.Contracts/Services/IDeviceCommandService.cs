using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.Services
{
    public interface IDeviceCommandService
    {
        IEnumerable<DeviceCommandVm> GetDeviceCommandVmList();
        DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId, string commandType);
        IEnumerable<Guid> GetActiveDeviceIds();

        Task<BlobResultDto> IssueCommandAsync(IssueDeviceCommandDto dto);

    }
}
