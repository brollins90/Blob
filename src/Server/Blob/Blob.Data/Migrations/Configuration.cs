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
            DeviceType testDeviceTypeWinDesktop = new DeviceType() { Value = "WindowsDesktop" };
            DeviceType testDeviceTypeWinServer = new DeviceType() { Value = "WindowsServer" };
            context.Set<DeviceType>().AddOrUpdate(x => x.Value, testDeviceTypeWinDesktop);
            context.Set<DeviceType>().AddOrUpdate(x => x.Value, testDeviceTypeWinServer);
            context.SaveChanges();

            // Customers
            Customer testCustomer1 = new Customer() { Name = "TestCustomer1" };
            context.Set<Customer>().AddOrUpdate(x => x.Name, testCustomer1);
            context.SaveChanges();

            // Devices
            Device testDevice1 = new Device()
                                 {
                                     Id = Guid.Parse("43116fba-c370-4880-8a98-826176219ba6"),
                                     CustomerId = testCustomer1.Id,
                                     DeviceName = "Test Windows Desktop 1",
                                     DeviceTypeId = testDeviceTypeWinDesktop.Id
                                 };
            context.Set<Device>().AddOrUpdate(x => x.Id, testDevice1);
            context.SaveChanges();

            // Status

            // StatusPerf

            // User
            User testUser1 = new User()
                             {
                                 CustomerId = testCustomer1.Id,
                                 Username = "user1"
                             };
            context.Set<User>().AddOrUpdate(x => x.Id, testUser1);
            context.SaveChanges();
        }
    }
}
