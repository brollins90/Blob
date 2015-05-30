namespace Blob.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequireMonitorId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StatusRecords", "MonitorId", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StatusRecords", "MonitorId", c => c.String());
        }
    }
}
