namespace Blob.Core.Models
{
    using System;
    using System.Collections.Generic;

    public class CustomerGroup
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CustomerGroupRole> Roles { get; set; } = new List<CustomerGroupRole>();
        public virtual ICollection<CustomerGroupUser> Users { get; set; } = new List<CustomerGroupUser>();
    }
}