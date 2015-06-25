namespace BMonitor.Configuration
{
    using System;

    public interface IBMonitorServiceConfiguration
    {
        Guid DeviceId { get; set; }
        bool EnableCommandConnection { get; set; }
        bool EnablePerformanceMonitoring { get; set; }
        bool EnableStatusMonitoring { get; set; }
        string MonitorPath { get; set; }
        string Password { get; set; }
        string Username { get; set; }
    }
}