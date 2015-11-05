using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.Services;
using EntityFramework.Extensions;
using log4net;

namespace Blob.Core.Services
{
    public class BlobDashboardManager : IDashboardService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;
        private readonly IDeviceService _deviceManager;
        private readonly IDeviceCommandService _deviceCommandManager;
        private readonly IStatusRecordService _statusRecordManager;

        public BlobDashboardManager(ILog log, BlobDbContext context, IDeviceService deviceManager, IDeviceCommandService deviceCommandManager, IStatusRecordService statusRecordManager)
        {
            _log = log;
            _log.Debug("Constructing BlobDashboardManager");
            _context = context;
            _deviceManager = deviceManager;
            _deviceCommandManager = deviceCommandManager;
            _statusRecordManager = statusRecordManager;
        }
    
        public async Task<DashCurrentConnectionsLargeVm> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            var activeDeviceConnections = _deviceCommandManager.GetActiveDeviceIds().ToList();
            IEnumerable<DeviceCommandVm> availableCommands = _deviceCommandManager.GetDeviceCommandVmList();
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            //var cust = _context.Customers.Where(x => x.Id.Equals(customerId));
            var count = _context.Devices.Where(x => activeDeviceConnections.Contains(x.Id)).FutureCount();
            var devices = _context.Devices.Where(x => activeDeviceConnections.Contains(x.Id))
                .Include("Customer")
                .OrderByDescending(x => x.AlertLevel).ThenBy(x => x.DeviceName)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new DashCurrentConnectionsLargeVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new DashCurrentConnectionsListItemVm
                {
                    AvailableCommands = availableCommands,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    DeviceId = x.Id,
                    DeviceName = x.DeviceName,
                    Status = _deviceManager.CalculateDeviceAlertLevel(x.Id)
                }),
            }).ConfigureAwait(false);
        }

        public async Task<DashDevicesLargeVm> GetDashDevicesLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            var activeDeviceConnections = _deviceCommandManager.GetActiveDeviceIds().ToList();
            IEnumerable<DeviceCommandVm> availableCommands = _deviceCommandManager.GetDeviceCommandVmList();
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            //var cust = _context.Customers.Where(x => x.Id.Equals(customerId));
            var count = _context.Devices.Where(x => x.CustomerId.Equals(searchId)).FutureCount();
            var devices = _context.Devices
                .Where(x => x.CustomerId.Equals(searchId))
                .OrderByDescending(x => x.AlertLevel).ThenBy(x => x.DeviceName)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);

            List<DashDevicesLargeListItemVm> dItems = new List<DashDevicesLargeListItemVm>();
            foreach (var x in devices)
            {
                var deviceRecent = await _statusRecordManager.GetDeviceRecentStatusAsync(x.Id);

                var li = new DashDevicesLargeListItemVm
                         {
                             AvailableCommands = (activeDeviceConnections.Contains(x.Id)) ? availableCommands : new List<DeviceCommandVm>(),
                             DeviceId = x.Id,
                             DeviceName = x.DeviceName,
                             Reason = ChangeStatusRecordsToReasonString(deviceRecent),
                             Recomendations = new string[] { "not yet" },
                             Status = ChangeStatusRecordsToStatusInt(deviceRecent)
                         };
                dItems.Add(li);
            }
            return await Task.FromResult(new DashDevicesLargeVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = dItems
            });
        }

        private string ChangeStatusRecordsToReasonString(IList<StatusRecordListItemVm> records)
        {
            return records.Where(s => s.Status != 0).Aggregate<StatusRecordListItemVm, string>(null, (accum, r) => accum + (accum == null ? accum : ", ") + r.CurrentValue);
        }

        private int ChangeStatusRecordsToStatusInt(IList<StatusRecordListItemVm> records)
        {
            return records.Select(statusRecord => statusRecord.Status).Concat(new[] { 0 }).Max();
        }
    }
}
