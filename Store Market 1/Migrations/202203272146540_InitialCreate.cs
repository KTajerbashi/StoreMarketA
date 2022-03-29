namespace Store_Market_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        AccessCode = c.String(),
                        Name = c.String(),
                        Family = c.String(),
                        RoleId = c.Int(nullable: false),
                        Phone = c.Long(nullable: false),
                        Email = c.String(),
                        Address = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        ProductID = c.String(),
                        Name = c.String(),
                        Family = c.String(),
                        RoleId = c.Int(nullable: false),
                        Phone = c.Long(nullable: false),
                        Email = c.String(),
                        Address = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BuyCost = c.Double(nullable: false),
                        Name = c.String(),
                        Family = c.String(),
                        RoleId = c.Int(nullable: false),
                        Phone = c.Long(nullable: false),
                        Email = c.String(),
                        Address = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.RoleAccesses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        access = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoleAccesses");
            DropTable("dbo.Customers");
            DropTable("dbo.Agents");
            DropTable("dbo.Admins");
        }
    }
}
