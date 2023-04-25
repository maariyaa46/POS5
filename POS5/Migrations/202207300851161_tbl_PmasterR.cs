namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_PmasterR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PmasterRs", "discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PmasterRs", "receivedamount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PmasterRs", "comment", c => c.String());
            AddColumn("dbo.PmasterRs", "balanceamount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PmasterRs", "grandtotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PmasterRs", "nettotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PmasterRs", "tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PmasterRs", "tax");
            DropColumn("dbo.PmasterRs", "nettotal");
            DropColumn("dbo.PmasterRs", "grandtotal");
            DropColumn("dbo.PmasterRs", "balanceamount");
            DropColumn("dbo.PmasterRs", "comment");
            DropColumn("dbo.PmasterRs", "receivedamount");
            DropColumn("dbo.PmasterRs", "discount");
        }
    }
}
