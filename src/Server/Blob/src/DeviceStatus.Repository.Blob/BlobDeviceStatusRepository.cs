namespace DeviceStatus.Repository.Blob
{
    using Core.Interfaces;
    using Core.Models;
    using global::Blob.Core;
    using System;
    using System.Collections.Generic;

    public class BlobDeviceStatusRepository : IDeviceStatusRepository
    {
        BlobDbContext _context;

        public BlobDeviceStatusRepository(BlobDbContext context)
        {
            _context = context;
        }

        public void AddPerformanceRecord(long statusRecordId, PerformanceRecord performanceRecord)
        {
            throw new NotImplementedException();
        }

        public void AddStatusRecord(StatusRecord record)
        {
            throw new NotImplementedException();
        }

        public ICollection<StatusRecord> GetAllStatusRecordsAndPerformanceForDevice(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public ICollection<StatusRecord> GetAllStatusRecordsForDevice(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public StatusRecord GetStatusRecordWithPerformance(long recordId)
        {
            throw new NotImplementedException();
        }

        public void RemoveStatusRecord(long recordId)
        {
            throw new NotImplementedException();
        }
    }
}
