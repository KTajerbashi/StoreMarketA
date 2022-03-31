namespace Store_Market_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agents", "CompanyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Agents", "CompanyName");
        }
    }
}
