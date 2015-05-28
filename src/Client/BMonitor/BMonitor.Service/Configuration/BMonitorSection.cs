using System.Configuration;

namespace BMonitor.Service.Configuration
{
    public class BMonitorSection : ConfigurationSection
    {
        public BMonitorSection() { }
        //public BMonitorSection() : this(DEFAULT_SECTION_NAME) { }

        //public BMonitorSection(string sectionName)
        //{
        //    _sectionName = sectionName;
        //}

        //private const string DEFAULT_SECTION_NAME = "BMonitor";
        //public string SectionName
        //{
        //    get
        //    {
        //        return string.IsNullOrEmpty(_sectionName) ? DEFAULT_SECTION_NAME : _sectionName;
        //    }
        //    set
        //    {
        //        _sectionName = value;
        //    }
        //}
        //private string _sectionName;

        //public void CreateDefaultConfigurationSection(string sectionName)
        //{
        //    BMonitorSection defaultSection = new BMonitorSection(sectionName);
        //    SettingsConfigurationCollection settingsCollection = new SettingsConfigurationCollection();
        //    settingsCollection[0] = new SettingConfigurationElement() { Key = "Element", Value = "Element value" };
        //    settingsCollection[1] = new SettingConfigurationElement() { Key = "NewElement", Value = "NeValueElement" };
        //    settingsCollection[2] = new SettingConfigurationElement() { Key = "NewElement2", Value = "NeValueElement2" };
        //    defaultSection.Settings = settingsCollection;
        //    CreateConfigurationSection(sectionName, defaultSection);
        //}

        //public void CreateConfigurationSection(string sectionName, ConfigurationSection section)
        //{
        //    var config = ConfigurationManager.OpenExeConfiguration(null);
        //    if (config.Sections[SectionName] == null)
        //    {
        //        config.Sections.Add(SectionName, section);
        //        section.SectionInformation.ForceSave = true;
        //        config.Save(ConfigurationSaveMode.Full);
        //    }
        //}

        //public override bool IsReadOnly()
        //{
        //    return false;
        //}

        //public void Save()
        //{
        //    Configuration config = ConfigurationManager.OpenExeConfiguration(null);
        //    BMonitorSection instance = (BMonitorSection)config.Sections[SectionName];
        //    instance.Settings = this.Settings;
        //    config.Save(ConfigurationSaveMode.Full);
        //}

        //public BMonitorSection GetSection()
        //{
        //    return ConfigurationManager.GetSection(SectionName);
        //}
        
        [ConfigurationProperty("service", IsRequired = true)]
        public ServiceElement Service
        {
            get { return (ServiceElement)base["service"]; }
        }

        [ConfigurationProperty("jobs", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(JobsCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public JobsCollection Jobs
        {
            get
            {
                JobsCollection jobsCollection = (JobsCollection)base["jobs"];
                return jobsCollection;
            }
        }
    }
}
