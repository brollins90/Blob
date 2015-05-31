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
    public class BlobStatusRecordManager : IStatusRecordService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;
        private BlobPerformanceRecordManager _performanceRecordManager;

        public BlobStatusRecordManager(ILog log, BlobDbContext context, BlobPerformanceRecordManager performanceRecordManager)
        {
            _log = log;
            _context = context;
            _performanceRecordManager = performanceRecordManager;
        }

        public DbSet<Device> Devices
        {
            get { return _context.Set<Device>(); }
        }

        public DbSet<StatusRecord> StatusRecords
        {
            get { return _context.Set<StatusRecord>(); }
        }



        public async Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId)
        {
            return await(from status in StatusRecords.Include("Devices")
                         where status.Id == recordId
                         select new StatusRecordDeleteVm
                         {
                             DeviceName = status.Device.DeviceName,
                             MonitorName = status.MonitorName,
                             RecordId = status.Id,
                             TimeGenerated = status.TimeGeneratedUtc

                         }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<StatusRecordPageVm> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10)
        {
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = StatusRecords.Where(x => x.DeviceId.Equals(deviceId)).FutureCount();
            var devices = StatusRecords
                .Where(x => x.DeviceId.Equals(deviceId))
                .OrderByDescending(x => x.TimeGeneratedUtc)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new StatusRecordPageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new StatusRecordListItemVm
                {
                    MonitorDescription = x.MonitorDescription,
                    MonitorName = x.MonitorName,
                    RecordId = x.Id,
                    Status = x.AlertLevel,
                    TimeGenerated = x.TimeGeneratedUtc
                }),
            }).ConfigureAwait(false);
        }

        public async Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId)
        {
            return await(from status in StatusRecords
                         where status.Id == recordId
                         select new StatusRecordSingleVm
                         {
                             MonitorDescription = status.MonitorDescription,
                             MonitorName = status.MonitorName,
                             RecordId = status.Id,
                             Status = status.AlertLevel,
                             TimeGenerated = status.TimeGeneratedUtc

                         }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<BlobResultDto> AddStatusRecordAsync(AddStatusRecordDto statusData)
        {
            _log.Debug("Storing status data " + statusData);
            Device device = Devices.Find(statusData.DeviceId);
            device.LastActivityDateUtc = DateTime.UtcNow;
            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            

            StatusRecord newStatus = new StatusRecord
            {
                AlertLevel = statusData.AlertLevel,
                CurrentValue = statusData.CurrentValue,
                DeviceId = statusData.DeviceId,
                MonitorDescription = statusData.MonitorDescription,
                MonitorId = statusData.MonitorId,
                MonitorName = statusData.MonitorName,
                TimeGeneratedUtc = statusData.TimeGenerated,
                TimeSentUtc = statusData.TimeSent,
            };
            StatusRecords.Add(newStatus);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            
            if (statusData.PerformanceRecordDto != null)
            {
                statusData.PerformanceRecordDto.StatusRecordId = newStatus.Id;
                await _performanceRecordManager.AddPerformanceRecordAsync(statusData.PerformanceRecordDto);
            }

            // now update the device alert level to reflect the new status

            var allMonPrev = await GetDeviceRecentStatusAsync(statusData.DeviceId);


            int dal = device.AlertLevel;
            int thisal = statusData.AlertLevel;
            var prev = allMonPrev.First(x => x.MonitorId.Equals(statusData.MonitorId));
            int preval = prev.Status;


            allMonPrev.Remove(prev);
            allMonPrev.Add(new StatusRecordListItemVm
                           {
                               MonitorDescription = newStatus.MonitorDescription,
                               MonitorName = newStatus.MonitorName,
                               MonitorId = newStatus.MonitorId,
                               Status = newStatus.AlertLevel,
                               TimeGenerated = newStatus.TimeGeneratedUtc
                           });
            int worstNow = allMonPrev.Select(statusRecord => statusRecord.Status).Concat(new[] { 0 }).Max();

            // just set it????
            device.AlertLevel = worstNow;
            _context.Entry(device).State = EntityState.Modified;

            //if (dal == 0)
            //{
            //    if (thisal == 0)
            //    {
            //        //await AddRec(statusData);
            //    }
            //    else
            //    {
            //        SetDeviceAlertLevel(device, thisal);
            //        //await AddRec(statusData);
            //    }
            //}
            //else // dal != 0
            //{
            //    if (preval != thisal) // this monitor changed
            //    {
            //        // check if this is the only bad status
            //        allMonPrev.Remove(prev);
            //        allMonPrev.Add(newStatus);
            //        int worstNow = allMonPrev.Select(statusRecord => statusRecord.AlertLevel).Concat(new[] { 0 }).Max();

            //        // just set it????
            //        SetDeviceAlertLevel(device, worstNow);
            //    }
            //    if (thisal > dal) // this one is worse
            //    {
            //        SetDeviceAlertLevel(device, thisal);
            //    }
            //}

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            StatusRecord status = StatusRecords.Find(dto.RecordId);
            _context.Entry(status).State = EntityState.Deleted;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            //await UpdateDeviceActivityTimeAsync(status.DeviceId);
            return BlobResultDto.Success;
        }


        public async Task<IList<StatusRecordListItemVm>> GetDeviceRecentStatusAsync(Guid deviceId)
        {
            return await (from s1 in StatusRecords
                          join s2 in
                              (
                                  from s in StatusRecords
                                  where s.DeviceId == deviceId
                                  group s by s.MonitorId
                                      into r
                                      select new { MonitorId = r.Key, TimeGeneratedUtc = r.Max(x => x.TimeGeneratedUtc) }
                                  )
                              on new { s1.MonitorId, s1.TimeGeneratedUtc } equals new { s2.MonitorId, s2.TimeGeneratedUtc }
                          select new StatusRecordListItemVm
                                 {
                                     MonitorDescription = s1.MonitorDescription,
                                     MonitorName = s1.MonitorName,
                                     RecordId = s1.Id,
                                     Status = s1.AlertLevel,
                                     TimeGenerated = s1.TimeGeneratedUtc
                                 }).ToListAsync();
        }
    }
}
