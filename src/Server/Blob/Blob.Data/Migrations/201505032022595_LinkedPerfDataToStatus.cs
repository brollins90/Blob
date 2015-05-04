namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkedPerfDataToStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StatusPerfs", "StatusId", c => c.Long(nullable: false));
            CreateIndex("dbo.StatusPerfs", "StatusId");
            AddForeignKey("dbo.StatusPerfs", "StatusId", "dbo.Statuses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusPerfs", "StatusId", "dbo.Statuses");
            DropIndex("dbo.StatusPerfs", new[] { "StatusId" });
            DropColumn("dbo.StatusPerfs", "StatusId");
        }
    }
}
