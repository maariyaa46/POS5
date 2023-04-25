namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_ItemLedger1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemLedgers", "SaleIN", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.ItemLedgers", "PurchaseIN", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.ItemLedgers", "SaleReturnOUT", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.ItemLedgers", "PurchaseReturnOUT", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            DropColumn("dbo.ItemLedgers", "IN1");
            DropColumn("dbo.ItemLedgers", "IN2");
            DropColumn("dbo.ItemLedgers", "OUT1");
            DropColumn("dbo.ItemLedgers", "OUT2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemLedgers", "OUT2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ItemLedgers", "OUT1", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ItemLedgers", "IN2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ItemLedgers", "IN1", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ItemLedgers", "PurchaseReturnOUT");
            DropColumn("dbo.ItemLedgers", "SaleReturnOUT");
            DropColumn("dbo.ItemLedgers", "PurchaseIN");
            DropColumn("dbo.ItemLedgers", "SaleIN");
        }
    }
}
