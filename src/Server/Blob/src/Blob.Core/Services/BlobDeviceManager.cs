namespace Blob.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Common.Services;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;
    using Extensions;
    using Models;
    using EntityFramework.Extensions;
    using log4net;

    public class BlobDeviceManager : IDeviceService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;
        private readonly BlobDeviceCommandManager _deviceCommandManager;
        private readonly BlobStatusRecordManager _statusRecordManager;

        public BlobDeviceManager(ILog log, BlobDbContext context, BlobDeviceCommandManager deviceCommandManager, BlobStatusRecordManager statusRecordManager)
        {
            _log = log;
            _log.Debug("Constructing BlobDeviceManager");
            _context = context;
            _deviceCommandManager = deviceCommandManager;
            _statusRecordManager = statusRecordManager;
        }

        public DbSet<Device> Devices { get { return _context.Set<Device>(); } }
        public DbSet<DeviceType> DeviceTypes { get { return _context.Set<DeviceType>(); } }


        public async Task<BlobResult> DisableDeviceAsync(DisableDeviceRequest dto)
        {
            _log.Debug(string.Format("DisableDeviceAsync({0})", dto.DeviceId));
            Device device = await _context.Devices.FindAsync(dto.DeviceId).ConfigureAwait(false);
            device.Enabled = false;

            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResult.Success;
        }

        public async Task<BlobResult> EnableDeviceAsync(EnableDeviceRequest dto)
        {
            _log.Debug(string.Format("EnableDeviceAsync({0})", dto.DeviceId));
            Device device = await _context.Devices.FindAsync(dto.DeviceId).ConfigureAwait(false);
            device.Enabled = true;

            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResult.Success;
        }

        public async Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest dto)
        {
            _log.Debug(string.Format("RegisterDeviceAsync({0})", dto.DeviceId));

            bool succeeded = false;
            Guid deviceId = Guid.Parse(dto.DeviceId);
            try
            {
                // check if device is already defined
                Device device = await _context.Devices.FirstOrDefaultAsync(x => x.Id.Equals(deviceId));
                if (device != null)
                {
                    throw new InvalidOperationException("This device has already been registered.");
                }

                DeviceType deviceType = await _context.Set<DeviceType>().FirstOrDefaultAsync(x => x.Name.Equals(dto.DeviceType));

                // todo, get the customerid from the principal
                ClaimsPrincipal id = ClaimsPrincipal.Current;
                Guid customerId = Guid.Parse(id.Identity.GetCustomerId());
                //Guid customerId = Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f");

                device = new Device
                {
                    AlertLevel = 0, // initially set to Ok
                    CreateDateUtc = DateTime.UtcNow,
                    CustomerId = customerId,
                    DeviceName = dto.DeviceName,
                    DeviceType = deviceType,
                    Enabled = true,
                    Id = deviceId,
                    LastActivityDateUtc = DateTime.UtcNow,
                };

                _context.Devices.Add(device);
                await _context.SaveChangesAsync();
                succeeded = true;
            }
            catch (Exception e)
            {
                _log.Error("Failed to register device", e);
                succeeded = false;
            }
            return new RegisterDeviceResponse
            {
                DeviceId = deviceId,
                Succeeded = succeeded,
                TimeSent = DateTime.UtcNow,
            };
        }

        public async Task<BlobResult> UpdateDeviceAsync(UpdateDeviceRequest dto)
        {
            _log.Debug(string.Format("UpdateDeviceAsync({0})", dto.DeviceId));
            Device device = Devices.Find(dto.DeviceId);
            device.DeviceName = dto.Name;
            device.DeviceTypeId = dto.DeviceTypeId;
            device.LastActivityDateUtc = DateTime.UtcNow;

            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return BlobResult.Success;
        }

        public async Task<DeviceDisableViewModel> GetDeviceDisableVmAsync(Guid deviceId)
        {
            _log.Debug(string.Format("GetDeviceDisableVmAsync({0})", deviceId));
            return await (from device in Devices
                          where device.Id == deviceId
                          select new DeviceDisableViewModel
                          {
                              DeviceId = device.Id,
                              DeviceName = device.DeviceName,
                              Enabled = device.Enabled
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<DeviceEnableViewModel> GetDeviceEnableVmAsync(Guid deviceId)
        {
            _log.Debug(string.Format("GetDeviceEnableVmAsync({0})", deviceId));
            return await (from device in Devices
                          where device.Id == deviceId
                          select new DeviceEnableViewModel
                          {
                              DeviceId = device.Id,
                              DeviceName = device.DeviceName,
                              Enabled = device.Enabled
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<DeviceSingleViewModel> GetDeviceSingleVmAsync(Guid deviceId)
        {
            _log.Debug(string.Format("GetDeviceSingleVmAsync({0})", deviceId));
            var d = await (from device in Devices.Include("DeviceType")
                           where device.Id == deviceId
                           select new DeviceSingleViewModel
                           {
                               CreateDate = device.CreateDateUtc,
                               DeviceId = device.Id,
                               DeviceName = device.DeviceName,
                               DeviceType = device.DeviceType.Name,
                               Enabled = device.Enabled,
                               LastActivityDate = device.LastActivityDateUtc,
                               //Status = device.AlertLevel,
                           }).SingleAsync();
            d.Status = CalculateDeviceAlertLevel(d.DeviceId);

            return d;
        }

        public async Task<DeviceUpdateViewModel> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            _log.Debug(string.Format("GetDeviceUpdateVmAsync({0})", deviceId));
            return await (from device in Devices.Include("DeviceTypes")
                          where device.Id == deviceId
                          select new DeviceUpdateViewModel
                          {
                              AvailableTypes = (from type in DeviceTypes
                                                select new DeviceTypeSingleViewModel
                                                {
                                                    DeviceTypeId = type.Id,
                                                    Value = type.Name
                                                }),
                              DeviceId = device.Id,
                              DeviceTypeId = device.DeviceTypeId,
                              DeviceName = device.DeviceName
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<DevicePageViewModel> GetDevicePageVmAsync(Guid customerId, int pageNum, int pageSize)
        {
            _log.Debug(string.Format("GetDevicePageVmAsync({0}, {1}, {2})", customerId, pageNum, pageSize));
            var activeDeviceConnections = _deviceCommandManager.GetActiveDeviceIds();

            IEnumerable<DeviceCommandViewModel> availableCommands = _deviceCommandManager.GetDeviceCommandVmList();
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = Devices.Where(x => x.CustomerId.Equals(customerId)).FutureCount();
            var devices = Devices
                .Include("DeviceType")
                .Where(x => x.CustomerId.Equals(customerId))
                .OrderByDescending(x => x.AlertLevel).ThenBy(x => x.DeviceName)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            var items = await Task.FromResult(new DevicePageViewModel
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new DeviceListItem
                {
                    AvailableCommands = (activeDeviceConnections.Contains(x.Id)) ? availableCommands : new List<DeviceCommandViewModel>(),
                    DeviceId = x.Id,
                    DeviceName = x.DeviceName,
                    DeviceType = x.DeviceType.Name,
                    Enabled = x.Enabled,
                    LastActivityDate = x.LastActivityDateUtc,
                    //Status = x.AlertLevel
                }),
            }).ConfigureAwait(false);
            foreach (var item in items.Items)
            {
                item.Status = CalculateDeviceAlertLevel(item.DeviceId);
            }
            return items;
        }

        public int CalculateDeviceAlertLevel(Guid deviceId)
        {
            _log.Debug(string.Format("CalculateDeviceAlertLevel({0})", deviceId));
            var records = _statusRecordManager.GetDeviceRecentStatusAsync(deviceId).Result;
            return records.Select(statusRecord => statusRecord.Status).Concat(new[] { 0 }).Max();
        }

        public async Task<BlobResult> AuthenticateDeviceAsync(AuthenticateDeviceRequest dto)
        {
            _log.Debug(string.Format("AuthenticateDevice({0})", dto.DeviceId));
            //Device device = await Devices.FindAsync(dto.DeviceId);

            //if (device.Id == dto.DeviceId)
            return await Task.FromResult(BlobResult.Success);
            //else return new BlobResultDto("Not Authenticated");
        }
    }
}