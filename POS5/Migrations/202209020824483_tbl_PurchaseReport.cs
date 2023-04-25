namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_PurchaseReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseReports", "tableName", c => c.String());
            AddColumn("dbo.PurchaseReports", "dateFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseReports", "dateTo", c => c.DateTime(nullable: false));
            DropColumn("dbo.PurchaseReports", "stock");
            DropColumn("dbo.PurchaseReports", "comment");
            DropColumn("dbo.PurchaseReports", "balanceamount");
            DropColumn("dbo.PurchaseReports", "grandtotal");
            DropColumn("dbo.PurchaseReports", "mid");
            DropColumn("dbo.PurchaseReports", "itemid");
            DropColumn("dbo.PurchaseReports", "itemname");
            DropColumn("dbo.PurchaseReports", "saleprice");
            DropColumn("dbo.PurchaseReports", "quantity");
            DropColumn("dbo.PurchaseReports", "total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseReports", "total", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.PurchaseReports", "quantity", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.PurchaseReports", "saleprice", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.PurchaseReports", "itemname", c => c.String());
            AddColumn("dbo.PurchaseReports", "itemid", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.PurchaseReports", "mid", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.PurchaseReports", "grandtotal", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.PurchaseReports", "balanceamount", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.PurchaseReports", "comment", c => c.String());
            AddColumn("dbo.PurchaseReports", "stock", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            DropColumn("dbo.PurchaseReports", "dateTo");
            DropColumn("dbo.PurchaseReports", "dateFrom");
            DropColumn("dbo.PurchaseReports", "tableName");
        }
    }
}
