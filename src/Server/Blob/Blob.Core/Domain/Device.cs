using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blob.Core.Domain
{
    public class Device
    {
        private ICollection<Status> _statuses;
        private ICollection<StatusPerf> _statusPerfs;

        public Guid Id { get; set; }
        public string DeviceName { get; set; }

        public long DeviceTypeId { get; set; }
        public virtual DeviceType DeviceType { get; set; }

        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Status> Statuses
        {
            get { return _statuses ?? (new Collection<Status>()); }
            protected set { _statuses = value; }
        }

        public virtual ICollection<StatusPerf> StatusPerfs
        {
            get { return _statusPerfs ?? (new Collection<StatusPerf>()); }
            protected set { _statusPerfs = value; }
        }
    }
}
