using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.Services
{
    public interface IDashboardService
    {
        Task<DashCurrentConnectionsLargeVm> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        Task<DashDevicesLargeVm> GetDashDevicesLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
    }
}
