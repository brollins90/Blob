namespace Blob.Contracts.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Request;

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
        public IEnumerable<NotificationScheduleListItem> AvailableSchedules { get; set; }

        public UpdateUserRequest ToRequest()
        {
            return new UpdateUserRequest { UserId = UserId, UserName = UserName, ScheduleId = ScheduleId };
        }
    }
}