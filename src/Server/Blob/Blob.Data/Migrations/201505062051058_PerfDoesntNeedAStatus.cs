namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PerfDoesntNeedAStatus : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StatusPerfs", new[] { "StatusId" });
            AlterColumn("dbo.StatusPerfs", "StatusId", c => c.Long());
            CreateIndex("dbo.StatusPerfs", "StatusId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StatusPerfs", new[] { "StatusId" });
            AlterColumn("dbo.StatusPerfs", "StatusId", c => c.Long(nullable: false));
            CreateIndex("dbo.StatusPerfs", "StatusId");
        }
    }
}
