﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Blob.Core.Models;

namespace Blob.Core.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BlobDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Blob.Data.BlobDbContext";
        }
        protected override void Seed(BlobDbContext context)
        {

            List<BlobPermission> permissions = new List<BlobPermission>() 
            {
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Create, Resource = BlobPermissions.Resources.Customer},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Delete, Resource = BlobPermissions.Resources.Customer},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Edit, Resource = BlobPermissions.Resources.Customer},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.View, Resource = BlobPermissions.Resources.Customer},

                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Create, Resource = BlobPermissions.Resources.Device},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Delete, Resource = BlobPermissions.Resources.Device},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Edit, Resource = BlobPermissions.Resources.Device},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.View, Resource = BlobPermissions.Resources.Device},

                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Create, Resource = BlobPermissions.Resources.PerformanceRecord},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Delete, Resource = BlobPermissions.Resources.PerformanceRecord},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Edit, Resource = BlobPermissions.Resources.PerformanceRecord},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.View, Resource = BlobPermissions.Resources.PerformanceRecord},

                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Create, Resource = BlobPermissions.Resources.StatusRecord},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Delete, Resource = BlobPermissions.Resources.StatusRecord},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Edit, Resource = BlobPermissions.Resources.StatusRecord},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.View, Resource = BlobPermissions.Resources.StatusRecord},

                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Create, Resource = BlobPermissions.Resources.User},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Delete, Resource = BlobPermissions.Resources.User},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.Edit, Resource = BlobPermissions.Resources.User},
                new BlobPermission { Id=Guid.NewGuid(), Operation = BlobPermissions.Operations.View, Resource = BlobPermissions.Resources.User},
            };

            foreach (var permission in permissions)
            {
                context.Set<BlobPermission>().AddOrUpdate(permission);
            }
            context.SaveChanges();

            // Roles
            List<Role> roles = new List<Role>() 
            {
                new Role {Id = Guid.Parse("E307DF88-839D-4E1B-A3CA-31AF459CB94E"), Name = "BlobAdmin"},
                new Role {Id = Guid.Parse("8EBC5306-37B3-402B-909E-F481845ACD02"), Name = "Customer"},
                new Role {Id = Guid.Parse("CA600C24-CB14-480C-B58C-6EEC393B507F"), Name = "CustomerAdmin"},
                new Role {Id = Guid.Parse("B6191FE1-C195-4BAF-8A7D-699616553636"), Name = "Device"},
            };

            foreach (var role in roles)
            {
                context.Set<Role>().AddOrUpdate(role);
            }
            context.SaveChanges();

            // DeviceTypes
            DeviceType testDeviceTypeWinDesktop = new DeviceType
            {
                Id = Guid.Parse("ca20309d-de49-460d-98f7-835ff2ea463d"),
                Name = "WindowsDesktop",
                Description = "Windows desktops"
            };
            DeviceType testDeviceTypeWinServer = new DeviceType
            {
                Id = Guid.Parse("99f1d6a5-74e1-4e1c-ba19-06be0a8345cf"),
                Name = "WindowsServer",
                Description = "Windows servers"
            };
            context.Set<DeviceType>().AddOrUpdate(x => x.Name, testDeviceTypeWinDesktop, testDeviceTypeWinServer);
            context.SaveChanges();


            // Customers
            Customer custBeforeService = new Customer
            {
                CreateDateUtc = DateTime.Parse("2015-04-01"),
                Id = Guid.Parse("94779853-6C22-4FAD-89E2-65A9BBF8C288"),
                Name = "Before Service",
                Enabled = true
            };
            context.Set<Customer>().AddOrUpdate(x => x.Name, custBeforeService);
            context.SaveChanges();


            // User
            User userBeforeService1 = new User
            {
                AccessFailedCount = 0,
                CreateDateUtc = DateTime.Parse("2015-04-01"),
                CustomerId = custBeforeService.Id,
                Email = "beforeService1@rritc.com",
                EmailConfirmed = true,
                Enabled = true,
                Id = Guid.Parse("5FE53DA7-938F-4CDC-9889-2A45B0A0541D"),
                LastActivityDate = DateTime.Parse("2015-04-01"),
                LockoutEnabled = false,
                LockoutEndDateUtc = DateTime.Parse("2015-04-01").AddDays(-1),
                PasswordHash = "BeforePassword",
                UserName = "BeforeUser"
            };
            context.Set<User>().AddOrUpdate(x => x.UserName, userBeforeService1);
            context.SaveChanges();

            //custBeforeService.CustomerUsers = new List<CustomerGroupUser>
            //                              {
            //                                  new CustomerGroupUser { CustomerId = custBeforeService.Id, UserId = userBeforeService1.Id }
            //                              };
            ////context.Entry(custBeforeService).State = EntityState.Modified;
            context.SaveChanges();

            Customer rritc = new Customer
            {
                CreateDateUtc = DateTime.Parse("2015-04-01"),
                Id = Guid.Parse("1568BBD4-251D-4C09-A09B-ECC421E44FB3"),
                Name = "RRITC",
                Enabled = true
            };
            context.Set<Customer>().AddOrUpdate(x => x.Name, rritc);
            context.SaveChanges();

            User rritc1 = new User
            {
                AccessFailedCount = 0,
                CreateDateUtc = DateTime.Parse("2015-04-01"),
                CustomerId = rritc.Id,
                Email = "rritc1@rritc.com",
                EmailConfirmed = true,
                Enabled = true,
                Id = Guid.Parse("22B9C84E-C3DF-42C5-BE9E-0F297B02EB5F"),
                LastActivityDate = DateTime.Parse("2015-04-01"),
                LockoutEnabled = false,
                LockoutEndDateUtc = DateTime.Parse("2015-04-01").AddDays(-1),
                PasswordHash = "password",
                UserName = "rritc1"
            };
            context.Set<User>().AddOrUpdate(x => x.UserName, rritc1);
            context.SaveChanges();

            // User
            User testUser1 = new User
            {
                AccessFailedCount = 0,
                CreateDateUtc = DateTime.Parse("2015-04-01"),
                CustomerId = rritc.Id,
                Email = "customerUser1@rritc.com",
                EmailConfirmed = true,
                Enabled = true,
                Id = Guid.Parse("4D5C23CB-E961-4D97-91D8-AAC2E8D0E2C1"),
                LastActivityDate = DateTime.Parse("2015-04-14"),
                LockoutEnabled = false,
                LockoutEndDateUtc = DateTime.Parse("2015-04-01").AddDays(-1),
                PasswordHash = "password",
                UserName = "customerUser1",
            };
            context.Set<User>().AddOrUpdate(x => x.Id, testUser1);
            context.SaveChanges();

            // Devices
            Device device1 = new Device
                                 {
                                     AlertLevel = 0,
                                     CreateDateUtc = DateTime.Parse("2015-04-01"),
                                     CustomerId = rritc.Id,
                                     DeviceName = "Test device 1",
                                     DeviceTypeId = testDeviceTypeWinDesktop.Id,
                                     Enabled = true,
                                     Id = Guid.Parse("1c6f0042-750e-4f5a-b1fa-41dd4ca9368a"),
                                     LastActivityDateUtc = DateTime.Parse("2015-04-14")
                                 };
        }

    }
}