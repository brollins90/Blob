namespace Blob.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Schedule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersProfiles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        SendEmailNotifications = c.Boolean(nullable: false),
                        EmailNotificationScheduleId = c.Guid(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.NotificationSchedules", t => t.EmailNotificationScheduleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.EmailNotificationScheduleId);
            
            CreateTable(
                "dbo.NotificationSchedules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersProfiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersProfiles", "EmailNotificationScheduleId", "dbo.NotificationSchedules");
            DropIndex("dbo.UsersProfiles", new[] { "EmailNotificationScheduleId" });
            DropIndex("dbo.UsersProfiles", new[] { "UserId" });
            DropTable("dbo.NotificationSchedules");
            DropTable("dbo.UsersProfiles");
        }
    }
}
