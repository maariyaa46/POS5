namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_277 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "name", c => c.String());
            DropColumn("dbo.Invoices", "invoicesummary");
            DropColumn("dbo.Invoices", "invoicedetail");
            DropColumn("dbo.Invoices", "itemdetail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "itemdetail", c => c.String());
            AddColumn("dbo.Invoices", "invoicedetail", c => c.String());
            AddColumn("dbo.Invoices", "invoicesummary", c => c.String());
            DropColumn("dbo.Invoices", "name");
        }
    }
}
