namespace Blob.Core.Services
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Services;
    using Contracts.ViewModel;
    using log4net;

    public class BlobNotificationScheduleManager : INotificationScheduleService
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;

        public BlobNotificationScheduleManager(ILog log, BlobDbContext context)
        {
            _log = log;
            _log.Debug("Constructing BlobNotificationScheduleService");
            _context = context;
        }

        public async Task<IEnumerable<NotificationScheduleListItem>> GetAllNotificationSchedules()
        {
            return await (from x in _context.NotificationSchedules
                          select new NotificationScheduleListItem
                          {
                              ScheduleId = x.Id,
                              Name = x.Name
                          }).ToListAsync().ConfigureAwait(false);
            //return new List<NotificationScheduleListItemVm> { new NotificationScheduleListItemVm { ScheduleId = Guid.Parse("76AA040A-253C-4AD3-838F-ADE186F40F47"), Name = "FirstSchedule" } };
        }
    }
}