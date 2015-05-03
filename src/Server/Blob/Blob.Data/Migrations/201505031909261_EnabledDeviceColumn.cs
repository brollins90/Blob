namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnabledDeviceColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "Enabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "Enabled");
        }
    }
}
