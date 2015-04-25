namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeyPairs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KeyPairs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AssociatedEntity = c.String(nullable: false, maxLength: 256),
                        PrivateKey = c.Binary(nullable: false, maxLength: 8000),
                        PublicKey = c.Binary(maxLength: 8000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KeyPairs");
        }
    }
}
