namespace Blob.Common.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.ViewModel;

    public interface INotificationScheduleService
    {
        Task<IEnumerable<NotificationScheduleListItem>> GetAllNotificationSchedules();
    }
}