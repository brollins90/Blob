using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.Services;
using Blob.Core.Command;
using Blob.Core.Extensions;
using Blob.Core.Models;
using EntityFramework.Extensions;
using log4net;

namespace Blob.Core.Services
{
    public class BlobDeviceManager : IDeviceService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;
        private readonly BlobDeviceCommandManager _deviceCommandManager;
        private BlobStatusRecordManager _statusRecordManager;

        public BlobDeviceManager(ILog log, BlobDbContext context, BlobDeviceCommandManager deviceCommandManager, BlobStatusRecordManager statusRecordManager)
        {
            _log = log;
            _context = context;
            _deviceCommandManager = deviceCommandManager;
            _statusRecordManager = statusRecordManager;
        }

        public DbSet<Device> Devices { get { return _context.Set<Device>(); } }
        public DbSet<DeviceType> DeviceTypes { get { return _context.Set<DeviceType>(); } }




        public async Task<BlobResultDto> DisableDeviceAsync(DisableDeviceDto dto)
        {
            Device device = await _context.Devices.FindAsync(dto.DeviceId).ConfigureAwait(false);
            device.Enabled = false;

            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> EnableDeviceAsync(EnableDeviceDto dto)
        {
            Device device = await _context.Devices.FindAsync(dto.DeviceId).ConfigureAwait(false);
            device.Enabled = true;

            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto)
        {
            _log.Debug("BlobManager registering device " + dto.DeviceId);

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
            return new RegisterDeviceResponseDto
            {
                DeviceId = deviceId,
                Succeeded = succeeded,
                TimeSent = DateTime.UtcNow,
            };
        }

        public async Task<BlobResultDto> UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            Device device = Devices.Find(dto.DeviceId);
            device.DeviceName = dto.Name;
            device.DeviceTypeId = dto.DeviceTypeId;
            device.LastActivityDateUtc = DateTime.UtcNow;

            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return BlobResultDto.Success;
        }

        public async Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId)
        {
            return await (from device in Devices
                          where device.Id == deviceId
                          select new DeviceDisableVm
                          {
                              DeviceId = device.Id,
                              DeviceName = device.DeviceName,
                              Enabled = device.Enabled
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId)
        {
            return await (from device in Devices
                          where device.Id == deviceId
                          select new DeviceEnableVm
                          {
                              DeviceId = device.Id,
                              DeviceName = device.DeviceName,
                              Enabled = device.Enabled
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            var d = await(from device in Devices.Include("DeviceType")
                          where device.Id == deviceId
                          select new DeviceSingleVm
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

        public async Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            return await(from device in Devices.Include("DeviceTypes")
                         where device.Id == deviceId
                         select new DeviceUpdateVm
                         {
                             AvailableTypes = (from type in DeviceTypes
                                               select new DeviceTypeSingleVm
                                               {
                                                   DeviceTypeId = type.Id,
                                                   Value = type.Name
                                               }),
                             DeviceId = device.Id,
                             DeviceTypeId = device.DeviceTypeId,
                             DeviceName = device.DeviceName
                         }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<DevicePageVm> GetDevicePageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            var activeDeviceConnections = _deviceCommandManager.GetActiveDeviceIds();

            IEnumerable<DeviceCommandVm> availableCommands = _deviceCommandManager.GetDeviceCommandVmList();
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = Devices.Where(x => x.CustomerId.Equals(customerId)).FutureCount();
            var devices = Devices
                .Include("DeviceType")
                .Where(x => x.CustomerId.Equals(customerId))
                .OrderByDescending(x => x.AlertLevel).ThenBy(x => x.DeviceName)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            var items = await Task.FromResult(new DevicePageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new DeviceListItemVm
                {
                    AvailableCommands = (activeDeviceConnections.Contains(x.Id)) ? availableCommands : new List<DeviceCommandVm>(),
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
            var records = _statusRecordManager.GetDeviceRecentStatusAsync(deviceId).Result;
            return records.Select(statusRecord => statusRecord.Status).Concat(new[] { 0 }).Max();
        }

        ////private async Task<int> GetCurrentAlertLevelAsync(Guid deviceId)
        ////{
        ////    IEnumerable<StatusRecord> records = await GetDeviceRecentStatusAsync(deviceId);
        ////    return ChangeStatusRecordsToStatusInt(records);
        ////}
        //private int GetCurrentAlertLevel(Guid deviceId)
        //{
        //    IEnumerable<StatusRecord> records = GetDeviceRecentStatus(deviceId);
        //    return ChangeStatusRecordsToStatusInt(records);
        //}

        ////private IEnumerable<StatusRecord> GetDeviceRecentStatus(Guid deviceId)
        ////{
        ////    using (BlobDbContext context = new BlobDbContext())
        ////    {
        ////        return (from s1 in context.DeviceStatuses
        ////                join s2 in
        ////                    (
        ////                        from s in context.DeviceStatuses
        ////                        where s.DeviceId == deviceId
        ////                        group s by s.MonitorId
        ////                            into r
        ////                            select new { MonitorId = r.Key, TimeGeneratedUtc = r.Max(x => x.TimeGeneratedUtc) }
        ////                    )
        ////                    on new { s1.MonitorId, s1.TimeGeneratedUtc } equals new { s2.MonitorId, s2.TimeGeneratedUtc }
        ////                select s1).ToList();
        ////    }
        ////}





    }
}
