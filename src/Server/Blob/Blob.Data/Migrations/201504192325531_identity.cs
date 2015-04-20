namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.DeviceSecurities", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.UserSecurities", "UserId", "dbo.Users");
            DropIndex("dbo.DeviceSecurities", new[] { "DeviceId" });
            DropIndex("dbo.UserSecurities", new[] { "UserId" });
            DropIndex("dbo.UserSecurities", "IX_UserSecurityEmail");
            DropIndex("dbo.UsersInRoles", new[] { "UserId" });
            DropIndex("dbo.UsersInRoles", new[] { "RoleId" });
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
            
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "PasswordHash", c => c.String());
            AddColumn("dbo.Users", "SecurityStamp", c => c.String());
            AddColumn("dbo.Users", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.Users", "PhoneNumber", c => c.String());
            AddColumn("dbo.Users", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            DropTable("dbo.DeviceSecurities");
            DropTable("dbo.UserSecurities");
            DropTable("dbo.UsersInRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersInRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
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
                .PrimaryKey(t => t.UserId);
            
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
                .PrimaryKey(t => t.DeviceId);
            
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserLogins", new[] { "User_Id" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropColumn("dbo.Users", "TwoFactorEnabled");
            DropColumn("dbo.Users", "PhoneNumberConfirmed");
            DropColumn("dbo.Users", "PhoneNumber");
            DropColumn("dbo.Users", "LockoutEndDateUtc");
            DropColumn("dbo.Users", "LockoutEnabled");
            DropColumn("dbo.Users", "AccessFailedCount");
            DropColumn("dbo.Users", "SecurityStamp");
            DropColumn("dbo.Users", "PasswordHash");
            DropColumn("dbo.Users", "EmailConfirmed");
            DropColumn("dbo.Users", "Email");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.UserRoles");
            CreateIndex("dbo.UsersInRoles", "RoleId");
            CreateIndex("dbo.UsersInRoles", "UserId");
            CreateIndex("dbo.UserSecurities", "Email", unique: true, name: "IX_UserSecurityEmail");
            CreateIndex("dbo.UserSecurities", "UserId");
            CreateIndex("dbo.DeviceSecurities", "DeviceId");
            AddForeignKey("dbo.UserSecurities", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.DeviceSecurities", "DeviceId", "dbo.Devices", "Id");
            AddForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
