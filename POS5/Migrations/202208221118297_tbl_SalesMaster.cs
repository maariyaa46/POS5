namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_SalesMaster : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemLedgers", "pur", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemLedgers", "pur");
        }
    }
}
