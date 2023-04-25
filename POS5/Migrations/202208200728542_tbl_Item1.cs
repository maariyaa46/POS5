namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_Item1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "catagoryname", c => c.String());
            AddColumn("dbo.Items", "brandname", c => c.String());
            DropColumn("dbo.Items", "catagoryid");
            DropColumn("dbo.Items", "brandid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "brandid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Items", "catagoryid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Items", "brandname");
            DropColumn("dbo.Items", "catagoryname");
        }
    }
}
