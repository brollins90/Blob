using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class UserUpdateVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid UserId { get; set; }

        [EmailAddress]
        [DataMember]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataMember]
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        [DataMember]
        [Display(Name = "Notification schedule")]
        [Required]
        public Guid ScheduleId { get; set; }

        [DataMember]
        public IEnumerable<NotificationScheduleListItemVm> AvailableSchedules { get; set; }

        public UpdateUserDto ToDto()
        {
            return new UpdateUserDto { UserId = this.UserId, UserName = this.UserName, ScheduleId = this.ScheduleId};
        }
    }
}
