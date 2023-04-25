namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_PdetailR : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PdetailRs", "purchaseprice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PdetailRs", "purchaseprice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
