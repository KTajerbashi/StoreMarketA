namespace Store_Market_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class percent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Percent", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Percent");
        }
    }
}
