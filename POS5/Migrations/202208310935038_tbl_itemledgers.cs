namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_itemledgers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemLedgers", "preturnid", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AddColumn("dbo.ItemLedgers", "sreturnid", c => c.Decimal(nullable: false, precision: 18, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemLedgers", "sreturnid");
            DropColumn("dbo.ItemLedgers", "preturnid");
        }
    }
}
