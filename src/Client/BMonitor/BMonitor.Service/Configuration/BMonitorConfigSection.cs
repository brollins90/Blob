using System.Configuration;

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
