namespace Blob.Core.Models
{
    using System;
    using System.Collections.Generic;

    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public bool Enabled { get; set; }

        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
        public virtual ICollection<CustomerGroup> Groups { get; set; } = new List<CustomerGroup>();
    }
}