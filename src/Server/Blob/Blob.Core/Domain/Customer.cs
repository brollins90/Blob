using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blob.Core.Domain
{
    public class Customer
    {
        private ICollection<Device> _devices;
        private ICollection<User> _users;

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Device> Devices
        {
            get { return _devices ?? (_devices = new Collection<Device>()); }
            protected set { _devices = value; }
        }

        public virtual ICollection<User> Users
        {
            get { return _users ?? (_users = new Collection<User>()); }
            protected set { _users = value; }
        }
    }
}
