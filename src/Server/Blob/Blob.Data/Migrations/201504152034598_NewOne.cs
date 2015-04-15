namespace Blob.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class NewOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_CustomerName");
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceName = c.String(nullable: false, maxLength: 256),
                        LastActivityDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeviceTypeId = c.Guid(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceTypes", t => t.DeviceTypeId, cascadeDelete: true)
                .Index(t => t.DeviceTypeId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.DeviceTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Value, unique: true, name: "IX_DeviceTypeValue");
            
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MonitorName = c.String(nullable: false, maxLength: 128),
                        MonitorDescription = c.String(maxLength: 256),
                        AlertLevel = c.Int(nullable: false),
                        TimeGenerated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TimeSent = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CurrentValue = c.String(nullable: false, maxLength: 4000),
                        DeviceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.StatusPerfs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MonitorName = c.String(nullable: false, maxLength: 128),
                        MonitorDescription = c.String(maxLength: 256),
                        TimeGenerated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Label = c.String(nullable: false, maxLength: 128),
                        UnitOfMeasure = c.String(nullable: false, maxLength: 128),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 5),
                        Warning = c.Decimal(precision: 18, scale: 5),
                        Critical = c.Decimal(precision: 18, scale: 5),
                        Min = c.Decimal(precision: 18, scale: 5),
                        Max = c.Decimal(precision: 18, scale: 5),
                        DeviceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false, maxLength: 256),
                        LastActivityDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CustomerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.Username, unique: true, name: "IX_UserUsername")
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_RoleName");
            
            CreateTable(
                "dbo.DeviceSecurities",
                c => new
                    {
                        DeviceId = c.Guid(nullable: false),
                        Key1 = c.String(nullable: false, maxLength: 128),
                        Key1Format = c.Int(nullable: false),
                        Key1Salt = c.String(nullable: false, maxLength: 128),
                        Key2 = c.String(nullable: false, maxLength: 128),
                        Key2Format = c.Int(nullable: false),
                        Key2Salt = c.String(nullable: false, maxLength: 128),
                        IsApproved = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastLoginDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastKey1ChangedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastKey2ChangedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        FailedLoginAttemptCount = c.Int(),
                        FailedLoginAttemptWindowStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Comment = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.DeviceId)
                .ForeignKey("dbo.Devices", t => t.DeviceId)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.UserSecurities",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Password = c.String(nullable: false, maxLength: 128),
                        PasswordFormat = c.Int(nullable: false),
                        PasswordSalt = c.String(nullable: false, maxLength: 128),
                        MobilePin = c.String(maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 256),
                        PasswordQuestion = c.String(maxLength: 256),
                        PasswordAnswer = c.String(maxLength: 128),
                        HasVerifiedEmail = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastLoginDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastPasswordChangedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastLockoutDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        FailedPasswordAttemptCount = c.Int(),
                        FailedPasswordAttemptWindowStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        FailedPasswordAnswerAttemptCount = c.Int(),
                        FailedPasswordAnswerAttemptWindowStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        Comment = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Email, unique: true, name: "IX_UserSecurityEmail");
            
            CreateTable(
                "dbo.UsersInRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSecurities", "UserId", "dbo.Users");
            DropForeignKey("dbo.DeviceSecurities", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.StatusPerfs", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Statuses", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Devices", "DeviceTypeId", "dbo.DeviceTypes");
            DropForeignKey("dbo.Devices", "CustomerId", "dbo.Customers");
            DropIndex("dbo.UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.UsersInRoles", new[] { "UserId" });
            DropIndex("dbo.UserSecurities", "IX_UserSecurityEmail");
            DropIndex("dbo.UserSecurities", new[] { "UserId" });
            DropIndex("dbo.DeviceSecurities", new[] { "DeviceId" });
            DropIndex("dbo.Roles", "IX_RoleName");
            DropIndex("dbo.Users", new[] { "CustomerId" });
            DropIndex("dbo.Users", "IX_UserUsername");
            DropIndex("dbo.StatusPerfs", new[] { "DeviceId" });
            DropIndex("dbo.Statuses", new[] { "DeviceId" });
            DropIndex("dbo.DeviceTypes", "IX_DeviceTypeValue");
            DropIndex("dbo.Devices", new[] { "CustomerId" });
            DropIndex("dbo.Devices", new[] { "DeviceTypeId" });
            DropIndex("dbo.Customers", "IX_CustomerName");
            DropTable("dbo.UsersInRoles");
            DropTable("dbo.UserSecurities");
            DropTable("dbo.DeviceSecurities");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.StatusPerfs");
            DropTable("dbo.Statuses");
            DropTable("dbo.DeviceTypes");
            DropTable("dbo.Devices");
            DropTable("dbo.Customers");
        }
    }
}
