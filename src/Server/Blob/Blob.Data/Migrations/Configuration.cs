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
            ////  This method will be called after migrating to the latest version.

            //// DeviceTypes
            //DeviceType testDeviceTypeWinDesktop = new DeviceType
            //                                      {
            //                                          Id = Guid.Parse("ca20309d-de49-460d-98f7-835ff2ea463d"),
            //                                          Value = "WindowsDesktop",
            //                                      };
            //DeviceType testDeviceTypeWinServer = new DeviceType
            //                                     {
            //                                         Id = Guid.Parse("99f1d6a5-74e1-4e1c-ba19-06be0a8345cf"),
            //                                         Value = "WindowsServer",
            //                                     };
            //context.Set<DeviceType>().AddOrUpdate(x => x.Value, testDeviceTypeWinDesktop, testDeviceTypeWinServer);
            //context.SaveChanges();

            //// Customers
            //Customer testCustomer1 = new Customer
            //                         {
            //                             Id = Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f"),
            //                             Name = "TestCustomer1",
            //                             CreateDate = DateTime.Parse("2015-04-14"),
            //                         };
            //context.Set<Customer>().AddOrUpdate(x => x.Name, testCustomer1);
            //context.SaveChanges();

            //// Devices
            //Device testDevice1 = new Device
            //                     {
            //                         //Customer,
            //                         CustomerId = testCustomer1.Id,
            //                         DeviceName = "Test Windows Desktop 1",
            //                         //DeviceType
            //                         DeviceTypeId = testDeviceTypeWinDesktop.Id,
            //                         Id = Guid.Parse("1c6f0042-750e-4f5a-b1fa-41dd4ca9368a"),
            //                         LastActivityDate = DateTime.Parse("2015-04-14"),
            //                     };
            //DeviceSecurity testDevice1Ds = new DeviceSecurity
            //                                {

            //                                    Comment = null,
            //                                    CreateDate = DateTime.Parse("2015-04-14"),
            //                                    Device = testDevice1,
            //                                    DeviceId = testDevice1.Id,
            //                                    //FailedLoginAttemptCount,
            //                                    //FailedLoginAttemptWindowStart,
            //                                    IsApproved = true,
            //                                    IsLockedOut = false,
            //                                    Key1 = "key1",
            //                                    Key1Format = 0,
            //                                    Key1Salt = "",
            //                                    Key2 = "key2",
            //                                    Key2Format = 0,
            //                                    Key2Salt = "",
            //                                    //LastKey1ChangedDate,
            //                                    //LastKey2ChangedDate,
            //                                    //LastLoginDate
            //                                };
            //context.Set<DeviceSecurity>().AddOrUpdate(x => new { x.DeviceId }, testDevice1Ds);
            //context.SaveChanges();

            //// Roles
            //Role deviceRole = new Role
            //                  {
            //                      Id = Guid.Parse("B6191FE1-C195-4BAF-8A7D-699616553636").ToString(),
            //                      Name = "Device"
            //                  };
            //Role adminRole = new Role
            //                 {
            //                     Id = Guid.Parse("E307DF88-839D-4E1B-A3CA-31AF459CB94E").ToString(),
            //                     Name = "BlobAdmin"
            //                 };
            //Role custAdminRole = new Role
            //                     {
            //                         Id = Guid.Parse("CA600C24-CB14-480C-B58C-6EEC393B507F").ToString(),
            //                         Name = "CustomerAdmin"
            //                     };
            //Role custRole = new Role
            //                {
            //                    Id = Guid.Parse("8EBC5306-37B3-402B-909E-F481845ACD02").ToString(),
            //                    Name = "Customer"
            //                };
            //context.Set<Role>().AddOrUpdate(x => x.Name, deviceRole, adminRole, custAdminRole, custRole);
            //context.SaveChanges();

            //// Status

            //// StatusPerf

            //// User
            //User testUser1 = new User
            //                 {
            //                     //Customer
            //                     //CustomerId = testCustomer1.Id,
            //                     Id = Guid.Parse("4D5C23CB-E961-4D97-91D8-AAC2E8D0E2C1").ToString(),
            //                     LastActivityDate = DateTime.Parse("2015-04-14"),
            //                     UserName = "customerUser1",
            //                 };
            //UserSecurity testUser1Us = new UserSecurity
            //                           {
            //                               Comment = null,
            //                               CreateDate = DateTime.Parse("2015-04-14"),
            //                               Email = "testUser1@test.com",
            //                               //FailedPasswordAnswerAttemptCount,
            //                               //FailedPasswordAnswerAttemptWindowStart,
            //                               //FailedPasswordAttemptCount,
            //                               //FailedPasswordAttemptWindowStart,
            //                               HasVerifiedEmail = true,
            //                               IsApproved = true,
            //                               IsLockedOut = false,
            //                               //LastLockoutDate,
            //                               //LastLoginDate,
            //                               //LastPasswordChangedDate,
            //                               //MobilePin,
            //                               //PasswordAnswer,
            //                               Password = "password",
            //                               PasswordFormat = 0,
            //                               //PasswordQuestion,
            //                               PasswordSalt = "",
            //                               //UserId = testUser1.Id,
            //                               User = testUser1,
            //                           };
            //context.Set<UserSecurity>().AddOrUpdate(x => new { x.UserId, x.Email }, testUser1Us);
            //context.SaveChanges();
        }
    }
}
