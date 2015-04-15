using System;
using System.Collections.Generic;

namespace Blob.Core.Domain
{
    public class User
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DateTime LastActivityDate { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
