using System;
using System.Configuration;

namespace BMonitor.Service.Configuration
{
    public class JobsCollection : ConfigurationElementCollection
    {
        public JobsCollection()
        {
            JobElement job = (JobElement)CreateNewElement();
            Add(job);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new JobElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((JobElement)element).Name;
        }

        public JobElement this[int index]
        {
            get
            {
                return (JobElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public JobElement this[string Name]
        {
            get
            {
                return (JobElement)BaseGet(Name);
            }
        }

        public int IndexOf(JobElement url)
        {
            return BaseIndexOf(url);
        }

        public void Add(JobElement url)
        {
            BaseAdd(url);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(JobElement url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
    //    private BMonitorJobsCollection() { }


    //    [ConfigurationProperty("job")]
    //    public BMonitorJobElement Job
    //    {
    //        get { return (BMonitorJobElement)this["job"]; }
    //    }

    //    protected override ConfigurationElement CreateNewElement()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override object GetElementKey(ConfigurationElement element)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
