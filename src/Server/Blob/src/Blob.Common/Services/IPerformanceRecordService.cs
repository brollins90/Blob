namespace Blob.Common.Services
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;

    public interface IPerformanceRecordService
    {
        Task<PerformanceRecordDeleteViewModel> GetPerformanceRecordDeleteVmAsync(long recordId);
        Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);
        Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum = 1, int pageSize = 10);
        Task<PerformanceRecordSingleViewModel> GetPerformanceRecordSingleVmAsync(long recordId);

        Task<BlobResult> AddPerformanceRecordAsync(AddPerformanceRecordRequest statusPerformanceData);
        Task<BlobResult> DeletePerformanceRecordAsync(DeletePerformanceRecordRequest dto);
    }
}