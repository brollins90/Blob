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
        Task<StatusRecordDeleteViewModel> GetStatusRecordDeleteViewModelAsync(long id);
        Task<StatusRecordPageViewModel> GetStatusRecordPageViewModelAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);
        Task<StatusRecordSingleViewModel> GetStatusRecordSingleViewModelAsync(long id);

        Task<IList<StatusRecordListItem>> GetDeviceRecentStatusAsync(Guid id);

        Task<MonitorListViewModel> GetMonitorListViewModelAsync(Guid id);

        Task<BlobResult> AddStatusRecordAsync(AddStatusRecordRequest request);
        Task<BlobResult> DeleteStatusRecordAsync(DeleteStatusRecordRequest request);
    }
}