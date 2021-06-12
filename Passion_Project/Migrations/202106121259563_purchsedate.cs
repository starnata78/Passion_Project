namespace Passion_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchsedate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "PurchaseDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contracts", "PurchaseDate");
        }
    }
}
