namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreEnableColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Enabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Enabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Enabled");
            DropColumn("dbo.Customers", "Enabled");
        }
    }
}
