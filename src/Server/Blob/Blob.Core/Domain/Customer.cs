using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blob.Core.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Enabled { get; set; }

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
