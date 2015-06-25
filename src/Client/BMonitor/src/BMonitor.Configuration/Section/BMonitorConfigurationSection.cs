namespace BMonitor.Configuration
{
    using System;
    using System.Configuration;

    public class BMonitorConfigurationSection : ConfigurationSection
    {
        public BMonitorConfigurationSection() { }

        [ConfigurationProperty("service", IsRequired = true)]
        public BMonitorServiceConfigurationElement Service
        {
            get { return (BMonitorServiceConfigurationElement)base["service"]; }
        }

        //[ConfigurationProperty("jobs", IsDefaultCollection = false)]
        //[ConfigurationCollection(typeof(BMonitorJobsConfigurationElementCollection),
        //    AddItemName = "add",
        //    ClearItemsName = "clear",
        //    RemoveItemName = "remove")]
        //public BMonitorJobsConfigurationElementCollection Jobs
        //{
        //    get
        //    {
        //        BMonitorJobsConfigurationElementCollection jobsCollection = (BMonitorJobsConfigurationElementCollection)base["jobs"];
        //        return jobsCollection;
        //    }
        //}
    }
}