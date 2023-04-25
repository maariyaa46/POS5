namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_Invoice : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Invoices");
            AlterColumn("dbo.Invoices", "id", c => c.Decimal(nullable: false, precision: 18, scale: 0, identity: true));
            AddPrimaryKey("dbo.Invoices", "id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Invoices");
            AlterColumn("dbo.Invoices", "id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Invoices", "id");
        }
    }
}
