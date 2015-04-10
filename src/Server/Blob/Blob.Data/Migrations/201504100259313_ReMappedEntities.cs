namespace Blob.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReMappedEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Devices", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Devices", "DeviceType_Id", "dbo.DeviceTypes");
            DropForeignKey("dbo.Users", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Devices", new[] { "Customer_Id" });
            DropIndex("dbo.Devices", new[] { "DeviceType_Id" });
            DropIndex("dbo.Users", new[] { "Customer_Id" });
            RenameColumn(table: "dbo.Devices", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.Devices", name: "DeviceType_Id", newName: "DeviceTypeId");
            RenameColumn(table: "dbo.Users", name: "Customer_Id", newName: "CustomerId");
            DropPrimaryKey("dbo.Devices");
            AddColumn("dbo.Devices", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.Devices", "DeviceName", c => c.String());
            AddColumn("dbo.DeviceTypes", "Value", c => c.String());
            AlterColumn("dbo.Devices", "CustomerId", c => c.Long(nullable: false));
            AlterColumn("dbo.Devices", "DeviceTypeId", c => c.Long(nullable: false));
            AlterColumn("dbo.StatusPerfs", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.StatusPerfs", "Warning", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.StatusPerfs", "Critical", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.StatusPerfs", "Min", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.StatusPerfs", "Max", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Users", "CustomerId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Devices", "Id");
            CreateIndex("dbo.Devices", "DeviceTypeId");
            CreateIndex("dbo.Devices", "CustomerId");
            CreateIndex("dbo.Statuses", "DeviceId");
            CreateIndex("dbo.StatusPerfs", "DeviceId");
            CreateIndex("dbo.Users", "CustomerId");
            AddForeignKey("dbo.Statuses", "DeviceId", "dbo.Devices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StatusPerfs", "DeviceId", "dbo.Devices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Devices", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Devices", "DeviceTypeId", "dbo.DeviceTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            DropColumn("dbo.Devices", "DeviceId");
            DropColumn("dbo.DeviceTypes", "Name");
            DropColumn("dbo.StatusPerfs", "TimeSent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StatusPerfs", "TimeSent", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeviceTypes", "Name", c => c.String());
            AddColumn("dbo.Devices", "DeviceId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Users", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Devices", "DeviceTypeId", "dbo.DeviceTypes");
            DropForeignKey("dbo.Devices", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.StatusPerfs", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Statuses", "DeviceId", "dbo.Devices");
            DropIndex("dbo.Users", new[] { "CustomerId" });
            DropIndex("dbo.StatusPerfs", new[] { "DeviceId" });
            DropIndex("dbo.Statuses", new[] { "DeviceId" });
            DropIndex("dbo.Devices", new[] { "CustomerId" });
            DropIndex("dbo.Devices", new[] { "DeviceTypeId" });
            DropPrimaryKey("dbo.Devices");
            AlterColumn("dbo.Users", "CustomerId", c => c.Long());
            AlterColumn("dbo.StatusPerfs", "Max", c => c.String());
            AlterColumn("dbo.StatusPerfs", "Min", c => c.String());
            AlterColumn("dbo.StatusPerfs", "Critical", c => c.String());
            AlterColumn("dbo.StatusPerfs", "Warning", c => c.String());
            AlterColumn("dbo.StatusPerfs", "Value", c => c.String());
            AlterColumn("dbo.Devices", "DeviceTypeId", c => c.Long());
            AlterColumn("dbo.Devices", "CustomerId", c => c.Long());
            DropColumn("dbo.DeviceTypes", "Value");
            DropColumn("dbo.Devices", "DeviceName");
            DropColumn("dbo.Devices", "Id");
            AddPrimaryKey("dbo.Devices", "DeviceId");
            RenameColumn(table: "dbo.Users", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.Devices", name: "DeviceTypeId", newName: "DeviceType_Id");
            RenameColumn(table: "dbo.Devices", name: "CustomerId", newName: "Customer_Id");
            CreateIndex("dbo.Users", "Customer_Id");
            CreateIndex("dbo.Devices", "DeviceType_Id");
            CreateIndex("dbo.Devices", "Customer_Id");
            AddForeignKey("dbo.Users", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Devices", "DeviceType_Id", "dbo.DeviceTypes", "Id");
            AddForeignKey("dbo.Devices", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
