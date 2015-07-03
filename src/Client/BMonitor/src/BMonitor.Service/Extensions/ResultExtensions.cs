using System;
using System.Linq;
using Blob.Contracts.Request;
using BMonitor.Common.Models;

namespace BMonitor.Service.Extensions
{
    public static class ResultExtensions
    {
        public static AddStatusRecordRequest ToAddStatusRecordDto(this ResultData result, Guid deviceId)
        {

            AddStatusRecordRequest statusDto = new AddStatusRecordRequest()
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
                AddPerformanceRecordRequest perfDto = new AddPerformanceRecordRequest
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
                    perfDto.Data.Add(new PerformanceRecordItem
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
