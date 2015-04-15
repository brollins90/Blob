using System;
using System.Collections.Generic;

namespace Blob.Core.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public DateTime LastActivityDate { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
