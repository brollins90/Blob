using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Blob.Contracts.Dto;
using Blob.Core.Domain;
using Blob.Data;
using Blob.Managers.Extensions;
using log4net;

namespace Blob.Managers.Status
{
    public interface IStatusManager
    {
        Task StoreStatusData(AddStatusRecordDto statusData);
        Task StoreStatusPerformanceData(AddPerformanceRecordDto statusPerformanceData);
    }

    public class StatusManager : IStatusManager
    {
        private readonly ILog _log;

        public StatusManager(BlobDbContext context, ILog log)
        {
            _log = log;
            _log.Debug("Constructing StatusManager");
            Context = context;
        }
        public BlobDbContext Context { get; private set; }

        
        public async Task StoreStatusData(AddStatusRecordDto statusData)
        {
            _log.Debug("Storing status data " + statusData);
            Device device = await Context.Devices.FirstOrDefaultAsync(x => x.Id.Equals(statusData.DeviceId));

            if (device != null)
            {
                Core.Domain.Status newStatus = new Core.Domain.Status
                           {
                               AlertLevel = statusData.AlertLevel,
                               CurrentValue = statusData.CurrentValue,
                               DeviceId = device.Id,
                               MonitorDescription = statusData.MonitorDescription,
                               MonitorName = statusData.MonitorName,
                               TimeGenerated = statusData.TimeGenerated,
                               TimeSent = statusData.TimeSent,
                           };
                Context.DeviceStatuses.Add(newStatus);
                await Context.SaveChangesAsync();

                if (statusData.PerformanceRecordDto != null)
                {
                    statusData.PerformanceRecordDto.StatusRecordId = newStatus.Id;
                    await StoreStatusPerformanceData(statusData.PerformanceRecordDto);
                }
            }
        }

        public async Task StoreStatusPerformanceData(AddPerformanceRecordDto statusPerformanceData)
        {
            _log.Debug("Storing status perf data " + statusPerformanceData);
            Device device = await Context.Devices.FirstOrDefaultAsync(x => x.Id.Equals(statusPerformanceData.DeviceId));

            if (device != null)
            {
                foreach (PerformanceRecordValue value in statusPerformanceData.Data)
                {
                    Context.DevicePerfDatas.Add(new StatusPerf
                                                      {
                                                          Critical = value.Critical.ToNullableDecimal(),
                                                          DeviceId = device.Id,
                                                          Label = value.Label,
                                                          Max = value.Max.ToNullableDecimal(),
                                                          Min = value.Min.ToNullableDecimal(),
                                                          MonitorDescription = statusPerformanceData.MonitorDescription,
                                                          MonitorName = statusPerformanceData.MonitorName,
                                                          StatusId = (statusPerformanceData.StatusRecordId.HasValue) ? statusPerformanceData.StatusRecordId.Value : 0,
                                                          TimeGenerated = statusPerformanceData.TimeGenerated,
                                                          UnitOfMeasure = value.UnitOfMeasure,
                                                          Value = value.Value.ToDecimal(),
                                                          Warning = value.Warning.ToNullableDecimal()
                                                      });
                    await Context.SaveChangesAsync();
                }
            }
        }
    }
}
