using System;
using System.Configuration;

namespace BMonitor.Service.Configuration
{
    public class JobElement : ConfigurationElement
    {
        public JobElement(string name, string monitorType)
        {
            Name = name;
            MonitorType = monitorType;
        }

        public JobElement()
        {

            this.Name = "FreeDiskSpace";
            this.MonitorType = "BMonitor.Monitors.FreeDiskSpace, BMonitor.Monitors";
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("monitorType", IsRequired = false)]
        public string MonitorType
        {
            get
            {
                return (string)this["monitorType"];
            }
            set
            {
                this["monitorType"] = value;
            }
        }
    }
}
