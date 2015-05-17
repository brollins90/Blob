﻿using System;
using System.Data.Entity;
using System.Reflection;
using System.Threading.Tasks;
using Blob.Contracts.Commands;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Core.Domain;
using Blob.Data;
using Blob.Managers.Command;
using Blob.Managers.Extensions;
using log4net;

namespace Blob.Managers.Blob
{
    public class BlobCommandManager : IBlobCommandManager
    {
        private readonly ILog _log;

        public BlobCommandManager(BlobDbContext context, ILog log)
        {
            _log = log;
            _log.Debug("Constructing BlobManager");
            Context = context;
        }
        protected BlobDbContext Context { get; private set; }

        protected ICommandConnectionManager ConnectionManager
        {
            get { return CommandConnectionManager.Instance; }
        }

        protected ICommandQueueManager QueueManager
        {
            get { return CommandQueueManager.Instance; }
        }


        // Customer
        public async Task DisableCustomerAsync(DisableCustomerDto dto)
        {
            Customer customer = Context.Customers.Find(dto.CustomerId);
            customer.Enabled = false;
            // todo: disable all devices and users for customer ?

            Context.Entry(customer).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EnableCustomerAsync(EnableCustomerDto dto)
        {
            Customer customer = Context.Customers.Find(dto.CustomerId);
            customer.Enabled = true;

            Context.Entry(customer).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            Customer customer = Context.Customers.Find(dto.CustomerId);
            customer.Name = dto.Name;

            Context.Entry(customer).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        // Device Command
        public async Task IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            string assemblyName = KnownCommandsMap.GetCommandHandlerInterfaceAssembly().FullName;
            Type commandType = Type.GetType(dto.Command + ", " + assemblyName);
            var cmdInstance = Activator.CreateInstance(commandType);
            PropertyInfo[] properties = commandType.GetProperties();

            foreach (var property in properties)
            {
                if (dto.CommandParameters.ContainsKey(property.Name))
                {
                    property.SetValue(cmdInstance, dto.CommandParameters[property.Name], null);
                }
            }

            // todo: add to issue table

            Guid commandId = Guid.NewGuid();

            bool queued = await QueueManager.QueueCommandAsync(dto.DeviceId, commandId, (cmdInstance as IDeviceCommand)).ConfigureAwait(false);
            if (!queued)
            {
                // todo: remove from table, or mark as not queued
            }
        }

        // Device
        public async Task DisableDeviceAsync(DisableDeviceDto dto)
        {
            Device device = await Context.Devices.FindAsync(dto.DeviceId).ConfigureAwait(false);
            device.Enabled = false;
            //device.LastActivityDate = DateTime.Now;

            Context.Entry(device).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        public async Task EnableDeviceAsync(EnableDeviceDto dto)
        {
            Device device = await Context.Devices.FindAsync(dto.DeviceId).ConfigureAwait(false);
            device.Enabled = true;
            //device.LastActivityDate = DateTime.Now;

            Context.Entry(device).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto message)
        {
            _log.Debug("BlobManager registering device " + message.DeviceId);
            // Authenticate user is done, it is required in the service

            Guid deviceId = Guid.Parse(message.DeviceId);
            // check if device is already defined
            Device device = await Context.Devices.FirstOrDefaultAsync(x => x.Id.Equals(deviceId));
            if (device != null)
            {
                throw new InvalidOperationException("This device has already been registered.");
            }

            DateTime createDate = DateTime.Now;
            DeviceType deviceType = await Context.Set<DeviceType>().FirstOrDefaultAsync(x => x.Value.Equals(message.DeviceType));

            // todo, get the customerid from the principal
            Guid customerId = Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f");
            
            device = new Device
                            {
                                AlertLevel = 0, // initially set to Ok
                                CreateDate = createDate,
                                CustomerId = customerId,
                                DeviceName = message.DeviceName,
                                DeviceType = deviceType,
                                Enabled = true,
                                Id = Guid.Parse(message.DeviceId),
                                LastActivityDate = createDate
                            };

            Context.Devices.Add(device);
            await Context.SaveChangesAsync();

            return new RegisterDeviceResponseDto
                                                 {
                                                     DeviceId = device.Id.ToString(),
                                                     TimeSent = DateTime.Now
                                                 };
        }


        public async Task UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            Device device = Context.Devices.Find(dto.DeviceId);
            device.DeviceName = dto.Name;
            device.DeviceTypeId = dto.DeviceTypeId;
            device.LastActivityDate = DateTime.Now;

            Context.Entry(device).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        // Performance Record

        public async Task AddPerformanceRecordAsync(AddPerformanceRecordDto statusPerformanceData)
        {
            _log.Debug("Storing status perf data " + statusPerformanceData);
            Device device = await Context.Devices.FirstOrDefaultAsync(x => x.Id.Equals(statusPerformanceData.DeviceId));
            device.LastActivityDate = DateTime.Now;
            Context.Entry(device).State = EntityState.Modified;

            if (device != null)
            {
                foreach (PerformanceRecordValue value in statusPerformanceData.Data)
                {
                    Context.DevicePerfDatas.Add(new StatusPerf
                    {
                        Critical = value.Critical.ToNullableDecimal(),
                        DeviceId = device.Id,
                        Label = value.Label,
                        Max = value.Max.ToNullableDecimal(),
                        Min = value.Min.ToNullableDecimal(),
                        MonitorDescription = statusPerformanceData.MonitorDescription,
                        MonitorName = statusPerformanceData.MonitorName,
                        StatusId = (statusPerformanceData.StatusRecordId.HasValue) ? statusPerformanceData.StatusRecordId.Value : 0,
                        TimeGenerated = statusPerformanceData.TimeGenerated,
                        UnitOfMeasure = value.UnitOfMeasure,
                        Value = value.Value.ToDecimal(),
                        Warning = value.Warning.ToNullableDecimal()
                    });
                    await Context.SaveChangesAsync();
                }
            }
        }

        public async Task DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            StatusPerf perf = Context.DevicePerfDatas.Find(dto.RecordId);
            Context.Entry(perf).State = EntityState.Deleted;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        // Status Record

        public async Task AddStatusRecordAsync(AddStatusRecordDto statusData)
        {
            _log.Debug("Storing status data " + statusData);
            Device device = await Context.Devices.FirstOrDefaultAsync(x => x.Id.Equals(statusData.DeviceId));
            device.LastActivityDate = DateTime.Now;
            Context.Entry(device).State = EntityState.Modified;

            if (device != null)
            {
                Core.Domain.Status newStatus = new Core.Domain.Status
                {
                    AlertLevel = statusData.AlertLevel,
                    CurrentValue = statusData.CurrentValue,
                    DeviceId = device.Id,
                    MonitorDescription = statusData.MonitorDescription,
                    MonitorName = statusData.MonitorName,
                    TimeGenerated = statusData.TimeGenerated,
                    TimeSent = statusData.TimeSent,
                };
                Context.DeviceStatuses.Add(newStatus);
                await Context.SaveChangesAsync();

                if (statusData.PerformanceRecordDto != null)
                {
                    statusData.PerformanceRecordDto.StatusRecordId = newStatus.Id;
                    await AddPerformanceRecordAsync(statusData.PerformanceRecordDto);
                }
            }
        }

        public async Task DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            Core.Domain.Status status = Context.DeviceStatuses.Find(dto.RecordId);
            Context.Entry(status).State = EntityState.Deleted;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        // User
        public async Task DisableUserAsync(DisableUserDto dto)
        {
            User user = Context.Users.Find(dto.UserId);
            user.Enabled = false;
            //user.LastActivityDate = DateTime.Now;

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EnableUserAsync(EnableUserDto dto)
        {
            User user = Context.Users.Find(dto.UserId);
            user.Enabled = true;

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateUserAsync(UpdateUserDto dto)
        {
            User user = Context.Users.Find(dto.UserId);
            // todo: can username change?
            //user.UserName = dto.UserName;
            if (!string.IsNullOrEmpty(dto.Email) && !user.Email.Equals(dto.Email))
            {
                user.Email = dto.Email;
                user.EmailConfirmed = false;
                user.LastActivityDate = DateTime.Now;
            }

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
