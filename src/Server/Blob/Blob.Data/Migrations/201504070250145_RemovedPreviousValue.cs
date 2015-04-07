namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedPreviousValue : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Statuses", "PreviousValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Statuses", "PreviousValue", c => c.String());
        }
    }
}
