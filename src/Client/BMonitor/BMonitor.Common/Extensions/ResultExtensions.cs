using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using BMonitor.Common.Models;

namespace BMonitor.Common.Extensions
{
    public static class ResultExtensions
    {
        public static AddStatusRecordDto ToAddStatusRecordDto(this ResultData result, Guid deviceId)
        {

            AddStatusRecordDto statusDto = new AddStatusRecordDto()
            {
                AlertLevel = (int)result.AlertLevel,
                CurrentValue = result.Value,
                DeviceId = deviceId,
                MonitorDescription = result.MonitorDescription,
                MonitorId = result.MonitorId,
                MonitorLabel = result.MonitorLabel,
                MonitorName = result.MonitorName,
                TimeGenerated = result.TimeGenerated,
                TimeSent = DateTime.Now
            };

            if (result.Perf.Any())
            {
                AddPerformanceRecordDto perfDto = new AddPerformanceRecordDto
                {
                    DeviceId = deviceId,
                    MonitorDescription = result.MonitorDescription,
                    MonitorId = result.MonitorId,
                    MonitorName = result.MonitorName,
                    TimeGenerated = result.TimeGenerated,
                    TimeSent = DateTime.Now
                };

                foreach (PerformanceData perf in result.Perf)
                {
                    perfDto.Data.Add(new PerformanceRecordValue
                    {
                        Critical = perf.Critical,
                        Label = perf.Label,
                        Max = perf.Max,
                        Min = perf.Min,
                        UnitOfMeasure = perf.UnitOfMeasure,
                        Value = perf.Value,
                        Warning = perf.Warning
                    });
                }
                statusDto.PerformanceRecordDto = perfDto;
            }

            return statusDto;

        }
    }
}
