namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMembership : DbMigration
    {
        public override void Up()
        {
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
                        User_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Email, unique: true, name: "IX_UserSecurityEmail")
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UsersInRoles",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            AddColumn("dbo.Customers", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Devices", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Users", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Users", "LastActivityDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Devices", "DeviceName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.DeviceTypes", "Value", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Statuses", "MonitorName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Statuses", "MonitorDescription", c => c.String(maxLength: 256));
            AlterColumn("dbo.Statuses", "TimeGenerated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Statuses", "TimeSent", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Statuses", "CurrentValue", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.StatusPerfs", "MonitorName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StatusPerfs", "MonitorDescription", c => c.String(maxLength: 256));
            AlterColumn("dbo.StatusPerfs", "TimeGenerated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.StatusPerfs", "Label", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StatusPerfs", "UnitOfMeasure", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.StatusPerfs", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 5));
            AlterColumn("dbo.StatusPerfs", "Warning", c => c.Decimal(precision: 18, scale: 5));
            AlterColumn("dbo.StatusPerfs", "Critical", c => c.Decimal(precision: 18, scale: 5));
            AlterColumn("dbo.StatusPerfs", "Min", c => c.Decimal(precision: 18, scale: 5));
            AlterColumn("dbo.StatusPerfs", "Max", c => c.Decimal(precision: 18, scale: 5));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 256));
            CreateIndex("dbo.Customers", "Name", unique: true, name: "IX_CustomerName");
            CreateIndex("dbo.DeviceTypes", "Value", unique: true, name: "IX_DeviceTypeValue");
            CreateIndex("dbo.Users", "UserId", unique: true, name: "IX_UserUserId");
            CreateIndex("dbo.Users", "Username", unique: true, name: "IX_UserUsername");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSecurities", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users");
            DropIndex("dbo.UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.UsersInRoles", new[] { "UserId" });
            DropIndex("dbo.UserSecurities", new[] { "User_Id" });
            DropIndex("dbo.UserSecurities", "IX_UserSecurityEmail");
            DropIndex("dbo.Roles", "IX_RoleName");
            DropIndex("dbo.Users", "IX_UserUsername");
            DropIndex("dbo.Users", "IX_UserUserId");
            DropIndex("dbo.DeviceTypes", "IX_DeviceTypeValue");
            DropIndex("dbo.Customers", "IX_CustomerName");
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.StatusPerfs", "Max", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.StatusPerfs", "Min", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.StatusPerfs", "Critical", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.StatusPerfs", "Warning", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.StatusPerfs", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.StatusPerfs", "UnitOfMeasure", c => c.String());
            AlterColumn("dbo.StatusPerfs", "Label", c => c.String());
            AlterColumn("dbo.StatusPerfs", "TimeGenerated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.StatusPerfs", "MonitorDescription", c => c.String());
            AlterColumn("dbo.StatusPerfs", "MonitorName", c => c.String());
            AlterColumn("dbo.Statuses", "CurrentValue", c => c.String());
            AlterColumn("dbo.Statuses", "TimeSent", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Statuses", "TimeGenerated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Statuses", "MonitorDescription", c => c.String());
            AlterColumn("dbo.Statuses", "MonitorName", c => c.String());
            AlterColumn("dbo.DeviceTypes", "Value", c => c.String());
            AlterColumn("dbo.Devices", "DeviceName", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            DropColumn("dbo.Users", "LastActivityDate");
            DropColumn("dbo.Users", "UserId");
            DropColumn("dbo.Devices", "CreateDate");
            DropColumn("dbo.Customers", "CreateDate");
            DropTable("dbo.UsersInRoles");
            DropTable("dbo.UserSecurities");
            DropTable("dbo.Roles");
        }
    }
}
