using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace Blob.Core.Identity
{
    public class GenericRole : GenericRole<string, GenericUserRole>
    {
        public GenericRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public GenericRole(string roleName) : this()
        {
            Name = roleName;
        }
    }

    public class GenericRole<TKey, TUserRole> : IRole<TKey>
        where TUserRole : GenericUserRole<TKey>
    {
        public GenericRole()
        {
            Users = new List<TUserRole>();
        }

        public virtual TKey Id { get; set; }
        public virtual string Name { get; set; }

        public virtual ICollection<TUserRole> Users { get; private set; }
    }
}
