using System;
using System.Collections.Generic;

namespace Blob.Core.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
