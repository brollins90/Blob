namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAlertLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Statuses", "AlertLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Statuses", "AlertLevel");
        }
    }
}
