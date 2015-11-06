using System;
using System.Collections.Generic;

namespace Blob.Core.Models
{
    public class Customer
    {
        public Customer()
        {
            Devices = new List<Device>();
            Groups = new List<CustomerGroup>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public bool Enabled { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<CustomerGroup> Groups { get; set; }
    }
}
