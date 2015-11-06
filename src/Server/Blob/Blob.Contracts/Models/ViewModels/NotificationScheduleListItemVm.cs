using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class NotificationScheduleListItemVm
    {
        [DataMember]
        [Required]
        public Guid ScheduleId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }
    }
}
