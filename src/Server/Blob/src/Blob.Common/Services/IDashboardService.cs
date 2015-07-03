namespace Blob.Common.Services
{
    using System;
    using System.Threading.Tasks;
    using Contracts.ViewModel;

    public interface IDashboardService
    {
        Task<DashCurrentConnectionsLargeViewModel> GetDashCurrentConnectionsLargeViewModelAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        Task<DashDevicesLargeViewModel> GetDashDevicesLargeViewModelAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
    }
}