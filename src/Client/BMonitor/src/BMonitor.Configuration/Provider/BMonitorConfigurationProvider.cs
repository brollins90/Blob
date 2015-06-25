namespace BMonitor.Configuration
{
    // http://www.codeproject.com/Articles/71843/Unit-Testing-your-App-config-and-ConfigurationSect

    using System;
    using System.Configuration;

    public class BMonitorConfigurationProvider : IBMonitorConfigurationProvider
    {
        /// <summary>
        /// Name of the section to read from the app.config.
        /// </summary>
        private string sectionName;

        /// <summary>
        /// Config object used to read a custom configuration file.
        /// If not set the default file will be used.
        /// </summary>
        private System.Configuration.Configuration config;

        public BMonitorConfigurationProvider()
        {
            sectionName = "BMonitor";
        }

        /// <summary>
        /// Set a custom configuration file.
        /// </summary>
        /// <param name="config"></param>
        public void SetConfigurationFile(string file)
        {
            // Create the mapping.
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = file;

            // Open the configuration.
            config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// Read the configuration file.
        /// </summary>
        /// <returns></returns>
        public IBMonitorServiceConfiguration Read()
        {
            // Try to read the config section.
            BMonitorConfigurationSection section = GetSection() as BMonitorConfigurationSection;
            if (section == null)
                throw new ConfigurationErrorsException();

            // Done.
            return section.Service;
        }

        /// <summary>
        /// Get the section from the default configuration file or from the custom one.
        /// </summary>
        /// <returns></returns>
        private object GetSection()
        {
            if (config != null)
                return config.GetSection(sectionName);
            else
                return ConfigurationManager.GetSection(sectionName);
        }
    }
}