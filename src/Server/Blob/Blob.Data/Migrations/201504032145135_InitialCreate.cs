namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Customer_Id = c.Long(),
                        DeviceType_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.DeviceTypes", t => t.DeviceType_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.DeviceType_Id);
            
            CreateTable(
                "dbo.DeviceTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statuses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DeviceId = c.Guid(nullable: false),
                        MonitorName = c.String(),
                        MonitorDescription = c.String(),
                        TimeGenerated = c.DateTime(nullable: false),
                        TimeSent = c.DateTime(nullable: false),
                        CurrentValue = c.String(),
                        PreviousValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Username = c.String(),
                        Customer_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Devices", "DeviceType_Id", "dbo.DeviceTypes");
            DropForeignKey("dbo.Devices", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Users", new[] { "Customer_Id" });
            DropIndex("dbo.Devices", new[] { "DeviceType_Id" });
            DropIndex("dbo.Devices", new[] { "Customer_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Statuses");
            DropTable("dbo.DeviceTypes");
            DropTable("dbo.Devices");
            DropTable("dbo.Customers");
        }
    }
}
