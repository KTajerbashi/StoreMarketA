namespace Store_Market_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accessCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        CompanyManager = c.String(),
                        Phone = c.Long(nullable: false),
                        Email = c.String(),
                        Address = c.String(),
                        Site = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        price = c.Int(nullable: false),
                        Totalprice = c.Int(nullable: false),
                        AgentID = c.Int(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Agents", "CompanyID", c => c.Int(nullable: false));
            DropColumn("dbo.Agents", "CompanyName");
            DropColumn("dbo.Agents", "ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agents", "ProductID", c => c.String());
            AddColumn("dbo.Agents", "CompanyName", c => c.String());
            DropColumn("dbo.Agents", "CompanyID");
            DropTable("dbo.Products");
            DropTable("dbo.Companies");
        }
    }
}
