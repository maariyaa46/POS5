namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_Brand : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "description");
            DropColumn("dbo.Categories", "price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Categories", "description", c => c.String());
        }
    }
}
