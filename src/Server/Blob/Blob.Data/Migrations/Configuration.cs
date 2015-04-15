using System;

namespace Blob.Data.Migrations
{
    using Core.Domain;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BlobDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Blob.Data.BlobDbContext";
        }

        protected override void Seed(BlobDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            // DeviceTypes
            DeviceType testDeviceTypeWinDesktop = new DeviceType { Value = "WindowsDesktop" };
            DeviceType testDeviceTypeWinServer = new DeviceType { Value = "WindowsServer" };
            context.Set<DeviceType>().AddOrUpdate(x => x.Value, testDeviceTypeWinDesktop);
            context.Set<DeviceType>().AddOrUpdate(x => x.Value, testDeviceTypeWinServer);
            context.SaveChanges();

            // Customers
            Customer testCustomer1 = new Customer
                                     {
                                         Name = "TestCustomer1",
                                         CreateDate = DateTime.Parse("2015-04-14")
                                     };
            context.Set<Customer>().AddOrUpdate(x => x.Name, testCustomer1);
            context.SaveChanges();

            // Devices
            Device testDevice1 = new Device
                                 {
                                     Id = Guid.Parse("43116fba-c370-4880-8a98-826176219ba6"),
                                     DeviceName = "Test Windows Desktop 1",
                                     CreateDate = DateTime.Parse("2015-04-14"),
                                     DeviceTypeId = testDeviceTypeWinDesktop.Id,
                                     CustomerId = testCustomer1.Id
                                 };
            context.Set<Device>().AddOrUpdate(x => x.Id, testDevice1);
            context.SaveChanges();

            // Roles
            Role deviceRole = new Role
                              {
                                  Id = Guid.Parse("B6191FE1-C195-4BAF-8A7D-699616553636"),
                                  Name = "Device"
                              };
            Role adminRole = new Role
                             {
                                 Id = Guid.Parse("E307DF88-839D-4E1B-A3CA-31AF459CB94E"),
                                 Name = "BlobAdmin"
                             };
            Role custAdminRole = new Role
                                 {
                                     Id = Guid.Parse("CA600C24-CB14-480C-B58C-6EEC393B507F"),
                                     Name = "CustomerAdmin"
                                 };
            Role custRole = new Role
                            {
                                Id = Guid.Parse("8EBC5306-37B3-402B-909E-F481845ACD02"),
                                Name = "Customer"
                            };
            context.Set<Role>().AddOrUpdate(x => x.Name, deviceRole);
            context.Set<Role>().AddOrUpdate(x => x.Name, adminRole);
            context.Set<Role>().AddOrUpdate(x => x.Name, custAdminRole);
            context.Set<Role>().AddOrUpdate(x => x.Name, custRole);
            context.SaveChanges();

            // Status

            // StatusPerf

            // User
            User testUser1 = new User
                             {
                                 UserId = Guid.Parse("4D5C23CB-E961-4D97-91D8-AAC2E8D0E2C1"),
                                 Username = "customerUser1",
                                 LastActivityDate = DateTime.Parse("2015-04-14"),
                                 CustomerId = testCustomer1.Id
                             };
            UserSecurity testUser1Us = new UserSecurity
                                       {
                                           UserId = testUser1.UserId,
                                           User = testUser1,
                                           Password = "password",
                                           PasswordFormat = 0,
                                           PasswordSalt = "",
                                           Email = "testUser1@test.com",
                                           HasVerifiedEmail = true,
                                           IsApproved = true,
                                           IsLockedOut = false,
                                           CreateDate = DateTime.Parse("2015-04-14"),
                                           Comment = null,
                                       };
            context.Set<UserSecurity>().AddOrUpdate(x => new { x.UserId, x.Email }, testUser1Us);
            context.SaveChanges();
        }
    }
}
