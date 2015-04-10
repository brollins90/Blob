namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDeviceIdToGuid : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Devices");
            AddColumn("dbo.Devices", "DeviceId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Devices", "DeviceId");
            DropColumn("dbo.Devices", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "Id", c => c.Long(nullable: false, identity: true));
            DropPrimaryKey("dbo.Devices");
            DropColumn("dbo.Devices", "DeviceId");
            AddPrimaryKey("dbo.Devices", "Id");
        }
    }
}
