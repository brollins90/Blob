using System;
using System.Collections.Generic;

namespace Blob.Core.Models
{
    public class CustomerGroup
    {
        public CustomerGroup()
        {
            Roles = new List<CustomerGroupRole>();
            Users = new List<CustomerGroupUser>();
        }

        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CustomerGroupRole> Roles { get; set; }
        public virtual ICollection<CustomerGroupUser> Users { get; set; }
    }
}
