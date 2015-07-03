namespace Blob.Core.Services
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Services;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;
    using Extensions;
    using Models;
    using EntityFramework.Extensions;
    using log4net;

    public class BlobPerformanceRecordManager : IPerformanceRecordService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;

        public BlobPerformanceRecordManager(ILog log, BlobDbContext context)
        {
            _log = log;
            _log.Debug("Constructing BlobPerformanceRecordManager");
            _context = context;
        }

        public DbSet<Device> Devices
        {
            get { return _context.Set<Device>(); }
        }

        public DbSet<StatusRecord> StatusRecords
        {
            get { return _context.Set<StatusRecord>(); }
        }

        public DbSet<PerformanceRecord> PerformanceRecords
        {
            get { return _context.Set<PerformanceRecord>(); }
        }



        public async Task<PerformanceRecordDeleteViewModel> GetPerformanceRecordDeleteViewModelAsync(long recordId)
        {
            _log.Debug(string.Format("GetPerformanceRecordDeleteVmAsync({0})", recordId));
            return await (from perf in PerformanceRecords.Include("Devices")
                          where perf.Id == recordId
                          select new PerformanceRecordDeleteViewModel
                          {
                              DeviceName = perf.Device.DeviceName,
                              MonitorName = perf.MonitorName,
                              RecordId = perf.Id,
                              TimeGenerated = perf.TimeGeneratedUtc
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageViewModelAsync(Guid deviceId, int pageNum, int pageSize)
        {
            _log.Debug(string.Format("GetPerformanceRecordPageVmAsync({0})", deviceId, pageNum, pageSize));
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = PerformanceRecords.Where(x => x.DeviceId.Equals(deviceId)).FutureCount();
            var devices = PerformanceRecords
                .Where(x => x.DeviceId.Equals(deviceId))
                .OrderByDescending(x => x.TimeGeneratedUtc)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new PerformanceRecordPageViewModel
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new PerformanceRecordListItem
                {
                    Critical = x.Critical.Value.ToString(),
                    Label = x.Label,
                    Max = x.Max.Value.ToString(),
                    Min = x.Min.Value.ToString(),
                    MonitorDescription = x.MonitorDescription,
                    MonitorName = x.MonitorName,
                    RecordId = x.Id,
                    TimeGenerated = x.TimeGeneratedUtc,
                    Unit = x.UnitOfMeasure,
                    Value = x.Value.ToString(),
                    Warning = x.Warning.Value.ToString()
                }),
            }).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageViewModelForStatusAsync(long recordId, int pageNum, int pageSize)
        {
            _log.Debug(string.Format("GetPerformanceRecordPageVmForStatusAsync({0})", recordId, pageNum, pageSize));
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = PerformanceRecords.Where(x => x.StatusId == recordId).FutureCount();
            var data = PerformanceRecords
                .Where(x => x.StatusId == recordId)
                .OrderByDescending(x => x.TimeGeneratedUtc)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new PerformanceRecordPageViewModel
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = data.Select(x => new PerformanceRecordListItem
                {
                    Critical = x.Critical.Value.ToString(),
                    Label = x.Label,
                    Max = x.Max.Value.ToString(),
                    Min = x.Min.Value.ToString(),
                    MonitorDescription = x.MonitorDescription,
                    MonitorName = x.MonitorName,
                    RecordId = x.Id,
                    TimeGenerated = x.TimeGeneratedUtc,
                    Unit = x.UnitOfMeasure,
                    Value = x.Value.ToString(),
                    Warning = x.Warning.Value.ToString()
                }),
            }).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordSingleViewModel> GetPerformanceRecordSingleViewModelAsync(long recordId)
        {
            _log.Debug(string.Format("GetPerformanceRecordSingleVmAsync({0})", recordId));
            return await (from perf in PerformanceRecords
                          where perf.Id == recordId
                          select new PerformanceRecordSingleViewModel
                          {
                              Critical = perf.Max.ToString(),
                              Label = perf.Label,
                              Max = perf.Max.ToString(),
                              Min = perf.Min.ToString(),
                              MonitorDescription = perf.MonitorDescription,
                              MonitorName = perf.MonitorName,
                              RecordId = perf.Id,
                              TimeGenerated = perf.TimeGeneratedUtc,
                              Unit = perf.UnitOfMeasure,
                              Value = perf.Value.ToString(),
                              Warning = perf.Warning.ToString()
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<BlobResult> AddPerformanceRecordAsync(AddPerformanceRecordRequest dto)
        {
            _log.Debug(string.Format("AddPerformanceRecordAsync({0} - {1})", dto.DeviceId, dto.MonitorId));
            _log.Debug("Storing status perf data " + dto);
            Device device = Devices.Find(dto.DeviceId);
            device.LastActivityDateUtc = DateTime.UtcNow;
            _context.Entry(device).State = EntityState.Modified;
            //await _context.SaveChangesAsync();


            //if (device != null)
            //{
            foreach (PerformanceRecordItem value in dto.Data)
            {
                PerformanceRecords.Add(new PerformanceRecord
                {
                    Critical = value.Critical.ToNullableDecimal(),
                    DeviceId = dto.DeviceId,// device.Id,
                    Label = value.Label,
                    Max = value.Max.ToNullableDecimal(),
                    Min = value.Min.ToNullableDecimal(),
                    MonitorDescription = dto.MonitorDescription,
                    MonitorName = dto.MonitorName,
                    StatusId = (dto.StatusRecordId.HasValue) ? dto.StatusRecordId.Value : 0,
                    TimeGeneratedUtc = dto.TimeGenerated,
                    UnitOfMeasure = value.UnitOfMeasure,
                    Value = value.Value.ToDecimal(),
                    Warning = value.Warning.ToNullableDecimal()
                });
                await _context.SaveChangesAsync();
            }
            //}
            return BlobResult.Success;
        }

        public async Task<BlobResult> DeletePerformanceRecordAsync(DeletePerformanceRecordRequest dto)
        {
            _log.Debug(string.Format("DeletePerformanceRecordAsync({0})", dto.RecordId));
            PerformanceRecord perf = PerformanceRecords.Find(dto.RecordId);
            _context.Entry(perf).State = EntityState.Deleted;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            //await UpdateDeviceActivityTimeAsync(perf.DeviceId);
            return BlobResult.Success;
        }
    }
}