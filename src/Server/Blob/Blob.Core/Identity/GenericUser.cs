﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace Blob.Core.Identity
{
    public class GenericUser : GenericUser<string, GenericUserLogin, GenericUserRole, GenericUserClaim>, IUser { }

    public class GenericUser<TKey, TLogin, TRole, TClaim> : IUser<TKey>
        where TLogin : GenericUserLogin<TKey>
        where TRole : GenericUserRole<TKey>
        where TClaim : GenericUserClaim<TKey>
    {

        public virtual TKey Id { get; set; }
        public virtual string UserName { get; set; }
        
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        
        public virtual string Email { get; set; }
        public virtual bool EmailConfirmed { get; set; }

        public virtual int AccessFailedCount { get; set; }
        public virtual DateTime? LastActivityDate { get; set; }
        public virtual bool LockoutEnabled { get; set; }
        public virtual DateTime? LockoutEndDateUtc { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual bool PhoneNumberConfirmed { get; set; }
        public virtual bool TwoFactorEnabled { get; set; }

        public virtual ICollection<TClaim> Claims { get; set; }
        public virtual ICollection<TLogin> Logins { get; set; }
        public virtual ICollection<TRole> Roles { get; set; }
    }
}