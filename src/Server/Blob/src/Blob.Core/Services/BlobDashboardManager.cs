namespace Blob.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Services;
    using Contracts.ViewModel;
    using EntityFramework.Extensions;
    using log4net;

    public class BlobDashboardManager : IDashboardService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;
        private readonly BlobDeviceManager _deviceManager;
        private readonly BlobDeviceCommandManager _deviceCommandManager;
        private readonly BlobStatusRecordManager _statusRecordManager;

        public BlobDashboardManager(ILog log, BlobDbContext context, BlobDeviceManager deviceManager, BlobDeviceCommandManager deviceCommandManager, BlobStatusRecordManager statusRecordManager)
        {
            _log = log;
            _log.Debug("Constructing BlobDashboardManager");
            _context = context;
            _deviceManager = deviceManager;
            _deviceCommandManager = deviceCommandManager;
            _statusRecordManager = statusRecordManager;
        }

        public async Task<DashCurrentConnectionsLargeViewModel> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            var activeDeviceConnections = _deviceCommandManager.GetActiveDeviceIds().ToList();
            IEnumerable<DeviceCommandViewModel> availableCommands = _deviceCommandManager.GetDeviceCommandVmList();
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            //var cust = _context.Customers.Where(x => x.Id.Equals(customerId));
            var count = _context.Devices.Where(x => activeDeviceConnections.Contains(x.Id)).FutureCount();
            var devices = _context.Devices.Where(x => activeDeviceConnections.Contains(x.Id))
                .Include("Customer")
                .OrderByDescending(x => x.AlertLevel).ThenBy(x => x.DeviceName)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new DashCurrentConnectionsLargeViewModel
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new DashCurrentConnectionsListItem
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

        public async Task<DashDevicesLargeViewModel> GetDashDevicesLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            var activeDeviceConnections = _deviceCommandManager.GetActiveDeviceIds().ToList();
            IEnumerable<DeviceCommandViewModel> availableCommands = _deviceCommandManager.GetDeviceCommandVmList();
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            //var cust = _context.Customers.Where(x => x.Id.Equals(customerId));
            var count = _context.Devices.Where(x => x.CustomerId.Equals(searchId)).FutureCount();
            var devices = _context.Devices
                .Where(x => x.CustomerId.Equals(searchId))
                .OrderByDescending(x => x.AlertLevel).ThenBy(x => x.DeviceName)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);

            List<DashDevicesListItem> dItems = new List<DashDevicesListItem>();
            foreach (var x in devices)
            {
                var deviceRecent = await _statusRecordManager.GetDeviceRecentStatusAsync(x.Id);

                var li = new DashDevicesListItem
                {
                    AvailableCommands = (activeDeviceConnections.Contains(x.Id)) ? availableCommands : new List<DeviceCommandViewModel>(),
                    DeviceId = x.Id,
                    DeviceName = x.DeviceName,
                    Reason = ChangeStatusRecordsToReasonString(deviceRecent),
                    Recomendations = new string[] { "not yet" },
                    Status = ChangeStatusRecordsToStatusInt(deviceRecent)
                };
                dItems.Add(li);
            }
            return await Task.FromResult(new DashDevicesLargeViewModel
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = dItems
            });
        }

        private string ChangeStatusRecordsToReasonString(IList<StatusRecordListItem> records)
        {
            return records.Where(s => s.Status != 0).Aggregate<StatusRecordListItem, string>(null, (accum, r) => accum + (accum == null ? accum : ", ") + r.CurrentValue);
        }

        private int ChangeStatusRecordsToStatusInt(IList<StatusRecordListItem> records)
        {
            return records.Select(statusRecord => statusRecord.Status).Concat(new[] { 0 }).Max();
        }
    }
}