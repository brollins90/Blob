using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blob.Core.Domain
{
    public class Customer
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime CreateDate { get; set; }

        public virtual ICollection<Device> Devices
        {
            get { return _devices ?? (_devices = new Collection<Device>()); }
            protected set { _devices = value; }
        }
        private ICollection<Device> _devices;

        public virtual ICollection<User> Users
        {
            get { return _users ?? (_users = new Collection<User>()); }
            protected set { _users = value; }
        }
        private ICollection<User> _users;
    }
}
