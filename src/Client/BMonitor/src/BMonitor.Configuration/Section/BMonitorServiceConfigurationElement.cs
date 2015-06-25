namespace BMonitor.Configuration
{
    using System;
    using System.Configuration;

    public class BMonitorServiceConfigurationElement : ConfigurationElement, IBMonitorServiceConfiguration
    {
        private BMonitorServiceConfigurationElement() { }

        public override bool IsReadOnly()
        {
            return false;
        }

        [ConfigurationProperty("deviceId", IsRequired = false)]
        public Guid DeviceId
        {
            get { return (Guid)base["deviceId"]; }
            set { base["deviceId"] = value; }
        }

        [ConfigurationProperty("enableCommandConnection", DefaultValue = false)]
        public bool EnableCommandConnection
        {
            get { return (bool)base["enableCommandConnection"]; }
            set { base["enableCommandConnection"] = value; }
        }

        [ConfigurationProperty("enablePerformanceMonitoring", DefaultValue = false)]
        public bool EnablePerformanceMonitoring
        {
            get { return (bool)base["enablePerformanceMonitoring"]; }
            set { base["enablePerformanceMonitoring"] = value; }
        }

        [ConfigurationProperty("enableStatusMonitoring", DefaultValue = false)]
        public bool EnableStatusMonitoring
        {
            get { return (bool)base["enableStatusMonitoring"]; }
            set { base["enableStatusMonitoring"] = value; }
        }

        [ConfigurationProperty("monitorPath", DefaultValue = "/Monitors/")]
        public string MonitorPath
        {
            get { return (string)base["monitorPath"]; }
            set { base["monitorPath"] = value; }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }

        [ConfigurationProperty("username")]
        public string Username
        {
            get { return (string)base["username"]; }
            set { base["username"] = value; }
        }
    }
}