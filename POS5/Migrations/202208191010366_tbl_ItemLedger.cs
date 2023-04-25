namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_ItemLedger : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemLedgers", "dateF", c => c.DateTime(nullable: false));
            AddColumn("dbo.ItemLedgers", "IN1", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.ItemLedgers", "IN2", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.ItemLedgers", "OUT1", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.ItemLedgers", "OUT2", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            DropColumn("dbo.ItemLedgers", "dateTo");
            DropColumn("dbo.ItemLedgers", "dateFrom");
            DropColumn("dbo.ItemLedgers", "PurchasedetailIN");
            DropColumn("dbo.ItemLedgers", "SalesReturnIN");
            DropColumn("dbo.ItemLedgers", "SalesOUT");
            DropColumn("dbo.ItemLedgers", "PurchaseDetailOUT");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemLedgers", "PurchaseDetailOUT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ItemLedgers", "SalesOUT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ItemLedgers", "SalesReturnIN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ItemLedgers", "PurchasedetailIN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ItemLedgers", "dateFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.ItemLedgers", "dateTo", c => c.DateTime(nullable: false));
            DropColumn("dbo.ItemLedgers", "OUT2");
            DropColumn("dbo.ItemLedgers", "OUT1");
            DropColumn("dbo.ItemLedgers", "IN2");
            DropColumn("dbo.ItemLedgers", "IN1");
            DropColumn("dbo.ItemLedgers", "dateF");
        }
    }
}
