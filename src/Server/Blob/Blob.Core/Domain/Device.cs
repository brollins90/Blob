using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blob.Core.Domain
{
    public class Device
    {
        public virtual Guid Id { get; set; }
        public virtual string DeviceName { get; set; }
        public virtual DateTime LastActivityDate { get; set; }

        public virtual DeviceType DeviceType { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Status> Statuses
        {
            get { return _statuses ?? (new Collection<Status>()); }
            protected set { _statuses = value; }
        }
        private ICollection<Status> _statuses;

        public virtual ICollection<StatusPerf> StatusPerfs
        {
            get { return _statusPerfs ?? (new Collection<StatusPerf>()); }
            protected set { _statusPerfs = value; }
        }
        private ICollection<StatusPerf> _statusPerfs;
    }
}
