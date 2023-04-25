namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_Category1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "img");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "img", c => c.String());
        }
    }
}
