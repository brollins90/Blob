namespace Blob.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MonitorLabel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StatusRecords", "MonitorLabel", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StatusRecords", "MonitorLabel");
        }
    }
}
