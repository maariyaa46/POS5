namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_PurchaseReport1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseReports", "invoicename", c => c.String());
            DropColumn("dbo.PurchaseReports", "date");
            DropColumn("dbo.PurchaseReports", "supplier");
            DropColumn("dbo.PurchaseReports", "grosstotal");
            DropColumn("dbo.PurchaseReports", "discount");
            DropColumn("dbo.PurchaseReports", "receivedamount");
            DropColumn("dbo.PurchaseReports", "nettotal");
            DropColumn("dbo.PurchaseReports", "tax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseReports", "tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseReports", "nettotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseReports", "receivedamount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseReports", "discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseReports", "grosstotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseReports", "supplier", c => c.String());
            AddColumn("dbo.PurchaseReports", "date", c => c.DateTime(nullable: false));
            DropColumn("dbo.PurchaseReports", "invoicename");
        }
    }
}
