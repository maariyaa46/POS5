namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_Brand1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brands", "name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Brands", "name", c => c.String(nullable: false));
        }
    }
}
