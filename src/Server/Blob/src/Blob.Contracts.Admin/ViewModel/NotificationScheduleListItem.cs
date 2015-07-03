namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class NotificationScheduleListItem
    {
        [DataMember]
        [Required]
        public Guid ScheduleId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }
    }
}