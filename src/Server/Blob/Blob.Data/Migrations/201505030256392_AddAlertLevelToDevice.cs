namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAlertLevelToDevice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "AlertLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "AlertLevel");
        }
    }
}
