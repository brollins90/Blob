using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Blob.Core.Domain
{
    public class Status
    {
        public long Id { get; set; }
        public string MonitorName { get; set; }
        public string MonitorDescription { get; set; }
        public int AlertLevel { get; set; }
        public DateTime TimeGenerated { get; set; }
        public DateTime TimeSent { get; set; }
        public string CurrentValue { get; set; }

        public Guid DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public virtual ICollection<StatusPerf> StatusPerfs
        {
            get { return _statusPerfs ?? (_statusPerfs = new Collection<StatusPerf>()); }
            protected set { _statusPerfs = value; }
        }
        private ICollection<StatusPerf> _statusPerfs;
    }
}
