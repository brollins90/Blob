namespace Blob.Common.Services
{
    using System;
    using System.Threading.Tasks;
    using Contracts.ViewModel;

    public interface IDashboardService
    {
        Task<DashCurrentConnectionsLargeViewModel> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        Task<DashDevicesLargeViewModel> GetDashDevicesLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
    }
}