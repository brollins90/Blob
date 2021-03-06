﻿using System;
using System.Collections.Generic;
using Blob.Core.Identity.Store;
using Microsoft.AspNet.Identity;

namespace Blob.Core.Identity.Models
{
    public class GenericRole : GenericRole<string, GenericUserRole>
    {
        public GenericRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public GenericRole(string roleName)
            : this()
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
