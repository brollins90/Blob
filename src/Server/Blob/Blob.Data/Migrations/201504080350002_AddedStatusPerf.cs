namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStatusPerf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusPerfs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DeviceId = c.Guid(nullable: false),
                        MonitorName = c.String(),
                        MonitorDescription = c.String(),
                        TimeGenerated = c.DateTime(nullable: false),
                        TimeSent = c.DateTime(nullable: false),
                        Label = c.String(),
                        Value = c.String(),
                        UnitOfMeasure = c.String(),
                        Warning = c.String(),
                        Critical = c.String(),
                        Min = c.String(),
                        Max = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StatusPerfs");
        }
    }
}
