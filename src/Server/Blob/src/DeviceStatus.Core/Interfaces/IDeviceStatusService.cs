namespace DeviceStatus.Core.Interfaces
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IDeviceStatusService
    {
        void AddStatusRecord(StatusRecord record);

        StatusRecord GetStatusRecord(long recordId);
        ICollection<StatusRecord> GetStatusRecordsForDevice(Guid deviceId);
        ICollection<StatusRecord> GetRecentStatusRecordsForDevice(Guid deviceId);

        void RemoveStatusRecord(long recordId);
    }
}