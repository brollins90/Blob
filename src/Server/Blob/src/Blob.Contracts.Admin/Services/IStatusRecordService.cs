using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.Services
{
    public interface IStatusRecordService
    {
        Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId);
        Task<StatusRecordPageVm> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);
        Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId);

        Task<IList<StatusRecordListItemVm>> GetDeviceRecentStatusAsync(Guid deviceId);

        Task<MonitorListVm> GetMonitorListVmAsync(Guid deviceId);

        Task<BlobResult> AddStatusRecordAsync(AddStatusRecordRequest statusData);
        Task<BlobResult> DeleteStatusRecordAsync(DeleteStatusRecordDto dto);
    }
}
