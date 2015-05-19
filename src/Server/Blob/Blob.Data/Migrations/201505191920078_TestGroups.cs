namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestGroups : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.UserGroups", "UserId");
            AddForeignKey("dbo.UserGroups", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGroups", "UserId", "dbo.Users");
            DropIndex("dbo.UserGroups", new[] { "UserId" });
        }
    }
}
