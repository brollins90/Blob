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

        Task<BlobResultDto> AddStatusRecordAsync(AddStatusRecordDto statusData);
        Task<BlobResultDto> DeleteStatusRecordAsync(DeleteStatusRecordDto dto);
    }
}
