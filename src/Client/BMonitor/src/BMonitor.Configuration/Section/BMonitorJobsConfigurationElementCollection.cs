//namespace BMonitor.Configuration
//{
//    using System.Configuration;

//    public class BMonitorJobsConfigurationElementCollection : ConfigurationElementCollection
//    {
//        public BMonitorJobsConfigurationElementCollection()
//        {
//            BMonitorJobConfigurationElement job = (BMonitorJobConfigurationElement)CreateNewElement();
//            Add(job);
//        }

//        public override ConfigurationElementCollectionType CollectionType
//        {
//            get
//            {
//                return ConfigurationElementCollectionType.AddRemoveClearMap;
//            }
//        }

//        protected override ConfigurationElement CreateNewElement()
//        {
//            return new BMonitorJobConfigurationElement();
//        }

//        protected override object GetElementKey(ConfigurationElement element)
//        {
//            return ((BMonitorJobConfigurationElement)element).Name;
//        }

//        public BMonitorJobConfigurationElement this[int index]
//        {
//            get
//            {
//                return (BMonitorJobConfigurationElement)BaseGet(index);
//            }
//            set
//            {
//                if (BaseGet(index) != null)
//                {
//                    BaseRemoveAt(index);
//                }
//                BaseAdd(index, value);
//            }
//        }

//        new public BMonitorJobConfigurationElement this[string Name]
//        {
//            get
//            {
//                return (BMonitorJobConfigurationElement)BaseGet(Name);
//            }
//        }

//        public int IndexOf(BMonitorJobConfigurationElement url)
//        {
//            return BaseIndexOf(url);
//        }

//        public void Add(BMonitorJobConfigurationElement url)
//        {
//            BaseAdd(url);
//        }

//        protected override void BaseAdd(ConfigurationElement element)
//        {
//            BaseAdd(element, false);
//        }

//        public void Remove(BMonitorJobConfigurationElement url)
//        {
//            if (BaseIndexOf(url) >= 0)
//                BaseRemove(url.Name);
//        }

//        public void RemoveAt(int index)
//        {
//            BaseRemoveAt(index);
//        }

//        public void Remove(string name)
//        {
//            BaseRemove(name);
//        }

//        public void Clear()
//        {
//            BaseClear();
//        }
//    }
//}
