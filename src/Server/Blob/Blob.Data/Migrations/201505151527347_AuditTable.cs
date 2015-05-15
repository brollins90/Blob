namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuditTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audit",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Initiator = c.String(nullable: false, maxLength: 128),
                        AuditLevel = c.Int(nullable: false),
                        Operation = c.String(nullable: false, maxLength: 128),
                        ResourceType = c.String(nullable: false, maxLength: 128),
                        Resource = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Audit");
        }
    }
}
