namespace DeviceStatus.Core
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;

    public class DeviceStatusService : IDeviceStatusService
    {
        IDeviceStatusRepository _repository;

        public DeviceStatusService(IDeviceStatusRepository repository)
        {
            _repository = repository;
        }

        public void AddStatusRecord(StatusRecord record)
        {
            _repository.AddStatusRecord(record);
        }

        public ICollection<StatusRecord> GetRecentStatusRecordsForDevice(Guid deviceId)
        {
            var records = GetStatusRecordsForDevice(deviceId);
            // TODO: more here
            return records;
        }

        public StatusRecord GetStatusRecord(long recordId)
        {
            return _repository.GetStatusRecordWithPerformance(recordId);
        }

        public ICollection<StatusRecord> GetStatusRecordsForDevice(Guid deviceId)
        {
            return _repository.GetAllStatusRecordsForDevice(deviceId);
        }

        public void RemoveStatusRecord(long recordId)
        {
            _repository.RemoveStatusRecord(recordId);
        }
    }
}