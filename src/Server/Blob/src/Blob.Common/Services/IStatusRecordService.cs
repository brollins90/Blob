namespace Blob.Common.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;

    public interface IStatusRecordService
    {
        Task<StatusRecordDeleteViewModel> GetStatusRecordDeleteVmAsync(long recordId);
        Task<StatusRecordPageViewModel> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);
        Task<StatusRecordSingleViewModel> GetStatusRecordSingleVmAsync(long recordId);

        Task<IList<StatusRecordListItem>> GetDeviceRecentStatusAsync(Guid deviceId);

        Task<MonitorListViewModel> GetMonitorListVmAsync(Guid deviceId);

        Task<BlobResult> AddStatusRecordAsync(AddStatusRecordRequest statusData);
        Task<BlobResult> DeleteStatusRecordAsync(DeleteStatusRecordRequest dto);
    }
}