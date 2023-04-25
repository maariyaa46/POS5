namespace POS5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tbl_Report : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true),
                        tableName = c.String(),
                        dateTo = c.DateTime(nullable: false),
                        dateFrom = c.DateTime(nullable: false),
                        view = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reports");
        }
    }
}
