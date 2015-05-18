using System;
using System.Collections.Generic;

// ReSharper disable DoNotCallOverridableMethodsInConstructor

namespace Blob.Identity
{
    public class GenericGroup : GenericGroup<string, GenericUserGroup, GenericGroupRole>
    {
        public GenericGroup()
        {
            Id = Guid.NewGuid().ToString();
        }

        public GenericGroup(string groupName)
            : this()
        {
            Name = groupName;
        }
    }

    public class GenericGroup<TKey, TUserGroup, TGroupRole> : IGroup<TKey>
        where TUserGroup : GenericUserGroup<TKey>
        where TGroupRole : GenericGroupRole<TKey>
    {
        public GenericGroup()
        {
            Roles = new List<TGroupRole>();
            Users = new List<TUserGroup>();
        }

        public virtual TKey Id { get; set; }
        public virtual string Name { get; set; }

        public virtual ICollection<TGroupRole> Roles { get; private set; }
        public virtual ICollection<TUserGroup> Users { get; private set; }
    }
}
