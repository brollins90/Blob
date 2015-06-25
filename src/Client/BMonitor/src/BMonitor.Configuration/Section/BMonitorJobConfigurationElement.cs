//namespace BMonitor.Configuration
//{
//    using System.Configuration;

//    public class BMonitorJobConfigurationElement : ConfigurationElement
//    {
//        public override bool IsReadOnly()
//        {
//            return false;
//        }

//        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
//        public string Name
//        {
//            get { return (string)this["name"]; }
//            set { this["name"] = value; }
//        }

//        [ConfigurationProperty("monitorType", IsRequired = true)]
//        public string MonitorType
//        {
//            get { return (string)this["monitorType"]; }
//            set { this["monitorType"] = value; }
//        }
//    }
//}
