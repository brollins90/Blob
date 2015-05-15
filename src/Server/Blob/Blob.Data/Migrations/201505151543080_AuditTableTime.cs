namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditTableTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Audit", "Time", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Audit", "Time");
        }
    }
}
