using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMonitor.Service.Configuration
{
    public class BMonitorConfigSection : ConfigurationSection
    {
        public BMonitorConfigSection() { }

        [ConfigurationProperty("main", IsRequired = true)]
        public BMonitorMainConfigElement Main
        {
            get { return (BMonitorMainConfigElement)base["main"]; }
        }

        //[ConfigurationProperty("Jobs")]
        //public BMonitorConfigurationElement Jobs
        //{
        //    get { return (BMonitorConfigurationElement)this["Jobs"]; }
        //}
    }
}
