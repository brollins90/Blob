using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Blob;
using Blob.Contracts.ViewModels;
using Blob.Data;
using log4net;

namespace Blob.Managers.Blob
{
    public class BlobQueryManager : IBlobQueryManager
    {
        private readonly ILog _log;

        public BlobQueryManager(BlobDbContext context, ILog log)
        {
            _log = log;
            _log.Debug("Constructing BlobQueryManager");
            Context = context;
        }
        public BlobDbContext Context { get; private set; }


        // Customer
        public async Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId)
        {
            return await (from cust in Context.Customers.Include("Devices").Include("DeviceTypes").Include("Users")
                          where cust.Id == customerId
                          select new CustomerSingleVm
                          {
                              CreateDate = cust.CreateDate,
                              CustomerId = cust.Id,
                              Name = cust.Name,
                              Devices = (from d in cust.Devices
                                         select new DeviceListVm
                                         {
                                             DeviceName = d.DeviceName,
                                             DeviceType = d.DeviceType.Value,
                                             DeviceId = d.Id,
                                             LastActivityDate = d.LastActivityDate,
                                             Status = d.AlertLevel
                                         }),
                              Users = (from u in cust.Users
                                       select new UserListVm
                                       {
                                           UserId = u.Id,
                                           UserName = u.UserName
                                       })
                          }).SingleAsync().ConfigureAwait(false);
        }

        // Device
        public async Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            return await (from device in Context.Devices.Include("DeviceStatuses").Include("DeviceTypes").Include("DevicePerfDatas")
                          where device.Id == deviceId
                          select new DeviceSingleVm
                          {
                              CreateDate = device.CreateDate,
                              DeviceId = device.Id,
                              DeviceName = device.DeviceName,
                              DeviceType = device.DeviceType.Value,
                              LastActivityDate = device.LastActivityDate,
                              PerformanceRecords = (from perf in device.StatusPerfs
                                                    select new PerformanceRecordListVm
                                                    {
                                                        Critical = perf.Max.ToString(),
                                                        Label = perf.Label,
                                                        Max = perf.Max.ToString(),
                                                        Min = perf.Min.ToString(),
                                                        MonitorDescription = perf.MonitorDescription,
                                                        MonitorName = perf.MonitorName,
                                                        RecordId = perf.Id,
                                                        TimeGenerated = perf.TimeGenerated,
                                                        Unit = perf.UnitOfMeasure,
                                                        Value = perf.Value.ToString(),
                                                        Warning = perf.Warning.ToString(),
                                                    }),
                              Status = device.AlertLevel,
                              StatusRecords = (from status in device.Statuses
                                               select new StatusRecordSingleVm
                                               {
                                                   MonitorDescription = status.MonitorDescription,
                                                   MonitorName = status.MonitorName,
                                                   RecordId = status.Id,
                                                   Status = status.AlertLevel,
                                                   TimeGenerated = status.TimeGenerated
                                               }),
                          }).SingleAsync().ConfigureAwait(false);
        }


        public async Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            return await (from device in Context.Devices.Include("DeviceTypes")
                          where device.Id == deviceId
                          select new DeviceUpdateVm
                          {
                              AvailableTypes = (from type in Context.DeviceTypes
                                                select new DeviceTypeSingleVm
                                           {
                                               DeviceTypeId = type.Id,
                                               Value = type.Value
                                           }),
                              DeviceId = device.Id,
                              DeviceTypeId = device.DeviceTypeId.ToString(),
                              Name = device.DeviceName
                          }).SingleAsync().ConfigureAwait(false);

        }

        // Status
        public async Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId)
        {
            return await (from status in Context.DeviceStatuses
                          where status.Id == recordId
                          select new StatusRecordSingleVm
                          {
                              MonitorDescription = status.MonitorDescription,
                              MonitorName = status.MonitorName,
                              RecordId = status.Id,
                              Status = status.AlertLevel,
                              TimeGenerated = status.TimeGenerated

                          }).SingleAsync().ConfigureAwait(false);
        }

        // Performance
        public async Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId)
        {
            return await (from perf in Context.DevicePerfDatas
                          where perf.Id == recordId
                          select new PerformanceRecordSingleVm
                          {
                              Critical = perf.Max.ToString(),
                              Label = perf.Label,
                              Max = perf.Max.ToString(),
                              Min = perf.Min.ToString(),
                              MonitorDescription = perf.MonitorDescription,
                              MonitorName = perf.MonitorName,
                              RecordId = perf.Id,
                              TimeGenerated = perf.TimeGenerated,
                              Unit = perf.UnitOfMeasure,
                              Value = perf.Value.ToString(),
                              Warning = perf.Warning.ToString()
                          }).SingleAsync().ConfigureAwait(false);
        }
    }
}
