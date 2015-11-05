namespace DeviceStatus.Core.Interfaces
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IDeviceStatusRepository
    {
        ICollection<StatusRecord> GetAllStatusRecordsForDevice(Guid deviceId);
        ICollection<StatusRecord> GetAllStatusRecordsAndPerformanceForDevice(Guid deviceId);

        StatusRecord GetStatusRecordWithPerformance(long recordId);

        void AddStatusRecord(StatusRecord record);
        void AddPerformanceRecord(long statusRecordId, PerformanceRecord performanceRecord);

        void RemoveStatusRecord(long recordId);
    }
}