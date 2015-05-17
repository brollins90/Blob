using System.Configuration;

namespace BMonitor.Service.Configuration
{
    public class BMonitorConfigSection : ConfigurationSection
    {
        public BMonitorConfigSection() { }

        [ConfigurationProperty("service", IsRequired = true)]
        public BMonitorServiceConfigElement Service
        {
            get { return (BMonitorServiceConfigElement)base["service"]; }
        }

        //[ConfigurationProperty("Jobs")]
        //public BMonitorJobsConfigElement Jobs
        //{
        //    get { return (BMonitorConfigurationElement)this["Jobs"]; }
        //}
    }
}
