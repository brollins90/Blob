using System.Configuration;

namespace BMonitor.Service.Configuration
{
    public class BMonitorSection : ConfigurationSection
    {
        public BMonitorSection() { }
        
        [ConfigurationProperty("service", IsRequired = true)]
        public ServiceElement Service
        {
            get { return (ServiceElement)base["service"]; }
        }

        //[ConfigurationProperty("jobs", IsDefaultCollection = false)]
        //[ConfigurationCollection(typeof(JobsCollection),
        //    AddItemName = "add",
        //    ClearItemsName = "clear",
        //    RemoveItemName = "remove")]
        //public JobsCollection Jobs
        //{
        //    get
        //    {
        //        JobsCollection jobsCollection = (JobsCollection)base["jobs"];
        //        return jobsCollection;
        //    }
        //}
    }
}
