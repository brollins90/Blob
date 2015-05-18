using System;
using System.Collections.Generic;

namespace Blob.Core.Domain
{
    public class BlobGroupRole
    {
        public Guid GroupId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class BlobUserGroup
    {
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
    }

    public class BlobGroup
    {
        public BlobGroup()
        {
            Id = Guid.NewGuid();
            GroupRoles = new List<BlobGroupRole>();
            GroupUsers = new List<BlobUserGroup>();
        }

        public BlobGroup(string groupName)
            : this()
        {
            Name = groupName;
        }

        public BlobGroup(string groupName, string description)
            : this(groupName)
        {
            Description = description;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<BlobGroupRole> GroupRoles { get; private set; }
        public virtual ICollection<BlobUserGroup> GroupUsers { get; private set; }
    }
}
