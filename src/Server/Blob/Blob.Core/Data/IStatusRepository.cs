using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Core.Domain;

namespace Blob.Core.Data
{
    public interface IStatusRepository
    {
        // Status
        Task AddStatusAsync(Device device, Status status);
        Task<IList<Status>> FindStatusesForDeviceAsync(Guid deviceId);
        Task<Status> GetStatusByIdAsync(long statusId);
        Task RemoveStatusAsync(long statusId);

        // Performance
        Task AddPerformanceAsync(Device device, StatusPerf statusPerf);
        Task<IList<StatusPerf>> FindPerformanceForDeviceAsync(Guid deviceId);
        Task<StatusPerf> GetPerformanceByIdAsync(long performanceId);
        Task RemovePerformanceAsync(long performanceId);
    }
}
