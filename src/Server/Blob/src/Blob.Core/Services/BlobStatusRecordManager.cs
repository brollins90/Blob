namespace Blob.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Services;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;
    using Models;
    using EntityFramework.Extensions;
    using log4net;

    public class BlobStatusRecordManager : IStatusRecordService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;
        private readonly BlobPerformanceRecordManager _performanceRecordManager;

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



        public async Task<StatusRecordDeleteViewModel> GetStatusRecordDeleteViewModelAsync(long recordId)
        {
            _log.Debug(string.Format("GetStatusRecordDeleteViewModelAsync({0})", recordId));
            return await (from status in StatusRecords.Include("Devices")
                          where status.Id == recordId
                          select new StatusRecordDeleteViewModel
                          {
                              DeviceName = status.Device.DeviceName,
                              MonitorName = status.MonitorName,
                              RecordId = status.Id,
                              TimeGenerated = status.TimeGeneratedUtc

                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<StatusRecordPageViewModel> GetStatusRecordPageViewModelAsync(Guid deviceId, int pageNum = 1, int pageSize = 10)
        {
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = StatusRecords.Where(x => x.DeviceId.Equals(deviceId)).FutureCount();
            var devices = StatusRecords
                .Where(x => x.DeviceId.Equals(deviceId))
                .OrderByDescending(x => x.TimeGeneratedUtc)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new StatusRecordPageViewModel
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new StatusRecordListItem
                {
                    MonitorDescription = x.MonitorDescription,
                    MonitorName = x.MonitorName,
                    RecordId = x.Id,
                    Status = x.AlertLevel,
                    TimeGenerated = x.TimeGeneratedUtc
                }),
            }).ConfigureAwait(false);
        }

        public async Task<StatusRecordSingleViewModel> GetStatusRecordSingleViewModelAsync(long recordId)
        {
            _log.Debug(string.Format("GetStatusRecordSingleViewModelAsync({0})", recordId));
            return await (from status in StatusRecords
                          where status.Id == recordId
                          select new StatusRecordSingleViewModel
                          {
                              MonitorDescription = status.MonitorDescription,
                              MonitorName = status.MonitorName,
                              RecordId = status.Id,
                              Status = status.AlertLevel,
                              TimeGenerated = status.TimeGeneratedUtc

                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<BlobResult> AddStatusRecordAsync(AddStatusRecordRequest dto)
        {
            _log.Debug(string.Format("AddStatusRecordAsync({0} - {1})", dto.DeviceId, dto.MonitorId));
            _log.Debug("Storing status data " + dto);
            Device device = Devices.Find(dto.DeviceId);
            device.LastActivityDateUtc = DateTime.UtcNow;
            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);


            StatusRecord newStatus = new StatusRecord
            {
                AlertLevel = dto.AlertLevel,
                CurrentValue = dto.CurrentValue,
                DeviceId = dto.DeviceId,
                MonitorDescription = dto.MonitorDescription,
                MonitorLabel = dto.MonitorLabel,
                MonitorId = dto.MonitorId,
                MonitorName = dto.MonitorName,
                TimeGeneratedUtc = dto.TimeGenerated,
                TimeSentUtc = dto.TimeSent,
            };
            StatusRecords.Add(newStatus);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            if (dto.PerformanceRecordDto != null)
            {
                dto.PerformanceRecordDto.StatusRecordId = newStatus.Id;
                await _performanceRecordManager.AddPerformanceRecordAsync(dto.PerformanceRecordDto);
            }

            // now update the device alert level to reflect the new status

            var allMonPrev = GetRecentStatus(dto.DeviceId).ToList();


            int deviceAlertLevel = device.AlertLevel;
            int newAlertLevel = dto.AlertLevel;
            var monitorPreviousRecord = allMonPrev.FirstOrDefault(x => x.MonitorId.Equals(dto.MonitorId));
            int monitorPreviousValue = (monitorPreviousRecord == null) ? 0 : monitorPreviousRecord.AlertLevel;


            allMonPrev.Remove(monitorPreviousRecord);
            allMonPrev.Add(newStatus);
            int worstNow = allMonPrev.Select(statusRecord => statusRecord.AlertLevel).Concat(new[] { 0 }).Max();

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
            return BlobResult.Success;
        }

        public async Task<BlobResult> DeleteStatusRecordAsync(DeleteStatusRecordRequest dto)
        {
            _log.Debug(string.Format("DeleteStatusRecordAsync({0})", dto.RecordId));
            StatusRecord status = StatusRecords.Find(dto.RecordId);
            _context.Entry(status).State = EntityState.Deleted;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            //await UpdateDeviceActivityTimeAsync(status.DeviceId);
            return BlobResult.Success;
        }


        private IList<StatusRecord> GetRecentStatus(Guid deviceId)
        {
            // todo: fix this cheating
            using (BlobDbContext con = new BlobDbContext())
            {
                _log.Debug(string.Format("GetRecentStatus({0})", deviceId));
                return (from s1 in con.DeviceStatuses
                        join s2 in
                            (
                                from s in con.DeviceStatuses
                                where s.DeviceId == deviceId
                                group s by s.MonitorId
                                    into r
                                select new { MonitorId = r.Key, TimeGeneratedUtc = r.Max(x => x.TimeGeneratedUtc) }
                            )
                            on new { s1.MonitorId, s1.TimeGeneratedUtc } equals new { s2.MonitorId, s2.TimeGeneratedUtc }
                        select s1).ToList();
            }
        }

        public async Task<IList<StatusRecordListItem>> GetDeviceRecentStatusAsync(Guid deviceId)
        {
            _log.Debug(string.Format("GetDeviceRecentStatusAsync({0})", deviceId));
            var s = await Task.FromResult(GetRecentStatus(deviceId));
            return s.Select(s1 =>
                          new StatusRecordListItem
                          {
                              CurrentValue = s1.CurrentValue,
                              MonitorDescription = s1.MonitorDescription,
                              MonitorLabel = s1.MonitorLabel,
                              MonitorName = s1.MonitorName,
                              RecordId = s1.Id,
                              Status = s1.AlertLevel,
                              TimeGenerated = s1.TimeGeneratedUtc
                          }).ToList();
        }

        public async Task<MonitorListViewModel> GetMonitorListViewModelAsync(Guid deviceId)
        {
            _log.Debug(string.Format("GetMonitorListViewModelAsync({0})", deviceId));
            var items = GetRecentStatus(deviceId).Select(s1 => new MonitorListListItem
            {
                CurrentValue = s1.CurrentValue,
                MonitorDescription = s1.MonitorDescription,
                MonitorId = s1.MonitorId,
                MonitorLabel = s1.MonitorLabel,
                MonitorName = s1.MonitorName,
                Status = s1.AlertLevel,
                TimeGenerated = s1.TimeGeneratedUtc
            });
            return await Task.FromResult(new MonitorListViewModel
            {
                Items = items
            });
        }


    }
}