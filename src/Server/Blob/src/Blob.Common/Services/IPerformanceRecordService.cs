namespace Blob.Common.Services
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;

    public interface IPerformanceRecordService
    {
        Task<PerformanceRecordDeleteViewModel> GetPerformanceRecordDeleteViewModelAsync(long id);
        Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageViewModelAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);
        Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageViewModelForStatusAsync(long id, int pageNum = 1, int pageSize = 10);
        Task<PerformanceRecordSingleViewModel> GetPerformanceRecordSingleViewModelAsync(long id);

        Task<BlobResult> AddPerformanceRecordAsync(AddPerformanceRecordRequest request);
        Task<BlobResult> DeletePerformanceRecordAsync(DeletePerformanceRecordRequest request);
    }
}