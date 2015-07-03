using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.Services
{
    public interface INotificationScheduleService
    {
        Task<IEnumerable<NotificationScheduleListItemVm>> GetAllNotificationSchedules();
    }
}
