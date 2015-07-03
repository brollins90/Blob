using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.Services
{
    public interface IPerformanceRecordService
    {
        Task<PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId);
        Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);
        Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum = 1, int pageSize = 10);
        Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId);

        Task<BlobResult> AddPerformanceRecordAsync(AddPerformanceRecordRequest statusPerformanceData);
        Task<BlobResult> DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto);
    }
}
