namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RecordTimeUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Initiator = c.String(nullable: false, maxLength: 128),
                        AuditLevel = c.Int(nullable: false),
                        Operation = c.String(nullable: false, maxLength: 128),
                        ResourceType = c.String(nullable: false, maxLength: 128),
                        Resource = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        CreateDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Enabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_CustomerName");
            
            CreateTable(
                "dbo.CustomerRoles",
                c => new
                    {
                        CustomerId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.RoleId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.CustomerUsers",
                c => new
                    {
                        CustomerId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.UserId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceName = c.String(nullable: false, maxLength: 256),
                        LastActivityDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        AlertLevel = c.Int(nullable: false),
                        CreateDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Enabled = c.Boolean(nullable: false),
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
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_DeviceTypeName");
            
            CreateTable(
                "dbo.PerformanceRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MonitorName = c.String(nullable: false, maxLength: 128),
                        MonitorDescription = c.String(maxLength: 256),
                        TimeGeneratedUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Label = c.String(nullable: false, maxLength: 128),
                        UnitOfMeasure = c.String(nullable: false, maxLength: 128),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 5),
                        Warning = c.Decimal(precision: 18, scale: 5),
                        Critical = c.Decimal(precision: 18, scale: 5),
                        Min = c.Decimal(precision: 18, scale: 5),
                        Max = c.Decimal(precision: 18, scale: 5),
                        DeviceId = c.Guid(nullable: false),
                        StatusId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.StatusRecords", t => t.StatusId)
                .Index(t => t.DeviceId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.StatusRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MonitorId = c.String(),
                        MonitorName = c.String(nullable: false, maxLength: 128),
                        MonitorDescription = c.String(maxLength: 256),
                        AlertLevel = c.Int(nullable: false),
                        TimeGeneratedUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TimeSentUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CurrentValue = c.String(nullable: false, maxLength: 4000),
                        DeviceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.DeviceId);
            
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
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDateUtc = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Enabled = c.Boolean(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        AccessFailedCount = c.Int(nullable: false),
                        LastActivityDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.UserName, unique: true, name: "IX_UserUsername");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.BlobPermissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Operation = c.String(nullable: false, maxLength: 128),
                        Resource = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PerformanceRecords", "StatusId", "dbo.StatusRecords");
            DropForeignKey("dbo.StatusRecords", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.PerformanceRecords", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Devices", "DeviceTypeId", "dbo.DeviceTypes");
            DropForeignKey("dbo.Devices", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerUsers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerRoles", "CustomerId", "dbo.Customers");
            DropIndex("dbo.UserLogins", new[] { "User_Id" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "IX_UserUsername");
            DropIndex("dbo.Users", new[] { "CustomerId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Roles", "IX_RoleName");
            DropIndex("dbo.StatusRecords", new[] { "DeviceId" });
            DropIndex("dbo.PerformanceRecords", new[] { "StatusId" });
            DropIndex("dbo.PerformanceRecords", new[] { "DeviceId" });
            DropIndex("dbo.DeviceTypes", "IX_DeviceTypeName");
            DropIndex("dbo.Devices", new[] { "CustomerId" });
            DropIndex("dbo.Devices", new[] { "DeviceTypeId" });
            DropIndex("dbo.CustomerUsers", new[] { "CustomerId" });
            DropIndex("dbo.CustomerRoles", new[] { "CustomerId" });
            DropIndex("dbo.Customers", "IX_CustomerName");
            DropTable("dbo.BlobPermissions");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.StatusRecords");
            DropTable("dbo.PerformanceRecords");
            DropTable("dbo.DeviceTypes");
            DropTable("dbo.Devices");
            DropTable("dbo.CustomerUsers");
            DropTable("dbo.CustomerRoles");
            DropTable("dbo.Customers");
            DropTable("dbo.AuditRecords");
        }
    }
}
