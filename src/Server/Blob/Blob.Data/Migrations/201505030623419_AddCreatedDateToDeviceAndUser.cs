namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedDateToDeviceAndUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Users", "CreateDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CreateDate");
            DropColumn("dbo.Devices", "CreateDate");
        }
    }
}
