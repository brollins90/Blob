namespace Blob.Common.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;

    public interface IDeviceCommandService
    {
        IEnumerable<DeviceCommandViewModel> GetDeviceCommandVmList();
        DeviceCommandIssueViewModel GetDeviceCommandIssueVm(Guid deviceId, string commandType);
        IEnumerable<Guid> GetActiveDeviceIds();

        Task<BlobResult> IssueCommandAsync(IssueDeviceCommandRequest dto);
    }
}