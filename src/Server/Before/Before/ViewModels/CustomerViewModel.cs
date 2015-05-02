using System;
using System.Collections.Generic;
using Before.Infrastructure.Extensions;
using Blob.Core.Domain;

namespace Before.ViewModels
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<UserViewModel> Users { get; set; }
        public virtual ICollection<DeviceViewModel> Devices { get; set; }

        public CustomerViewModel() { }

        public CustomerViewModel(Customer customer)
        {
            CreateDate = customer.CreateDate;
            Id = customer.Id;
            Name = customer.Name;
            Devices = new List<DeviceViewModel>();
            Users = new List<UserViewModel>();

            foreach (var device in customer.Devices)
            {
                var d = new DeviceViewModel
                {
                    CurrentStatus = StatusValue.Unknown,
                    DeviceName = device.DeviceName,
                    DeviceType = string.Empty, //device.DeviceType.ToString(),
                    Id = device.Id,
                    LastActivityDate = device.LastActivityDate,
                    PerformanceRecords = new List<PerformanceRecordViewModel>(),
                    StatusRecords = new List<StatusRecordViewModel>()
                };

                foreach (var status in device.Statuses)
                {
                    d.StatusRecords.Add(new StatusRecordViewModel
                    {
                        Id = status.Id,
                        MonitorDescription = status.MonitorDescription,
                        MonitorName = status.MonitorName,
                        Status = EnumUtils.ParseEnum<StatusValue>(status.CurrentValue),
                        TimeGenerated = status.TimeGenerated,
                        TimeSent = status.TimeSent
                    });
                }

                foreach (var perfRec in device.StatusPerfs)
                {
                    d.PerformanceRecords.Add(new PerformanceRecordViewModel
                    {
                        Critical = perfRec.Critical.ToString(),
                        Id = perfRec.Id,
                        Label = perfRec.Label,
                        Max = perfRec.Max.ToString(),
                        Min = perfRec.Min.ToString(),
                        MonitorDescription = perfRec.MonitorDescription,
                        MonitorName = perfRec.MonitorName,
                        TimeGenerated = perfRec.TimeGenerated,
                        Unit = perfRec.UnitOfMeasure,
                        Value = perfRec.Value.ToString(),
                        Warning = perfRec.Warning.ToString(),
                    });
                }
                Devices.Add(d);
            }

            foreach (var x in customer.Users)
            {
                Users.Add(new UserViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName
                });
            }
        }
    }
}
