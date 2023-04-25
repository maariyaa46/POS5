namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_245 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseReports", "view", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseReports", "view");
        }
    }
}
