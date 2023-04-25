namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "brandid", c => c.Decimal(nullable: false, precision: 18, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "brandid");
        }
    }
}
