using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Core.Domain;

namespace Blob.Core.Data
{
    public interface IStatusRepository
    {
        // Status
        Task<DataOpResult> AddStatusAsync(Device device, Status status);
        Task<IList<Status>> FindStatusesForDeviceAsync(Guid deviceId);
        Task<Status> GetStatusByIdAsync(long statusId);
        Task<DataOpResult> RemoveStatusAsync(long statusId);

        // Performance
        Task<DataOpResult> AddPerformanceAsync(Device device, StatusPerf statusPerf);
        Task<IList<StatusPerf>> FindPerformanceForDeviceAsync(Guid deviceId);
        Task<StatusPerf> GetPerformanceByIdAsync(long performanceId);
        Task<DataOpResult> RemovePerformanceAsync(long performanceId);
    }
}
