namespace Passion_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insurers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Insurers",
                c => new
                    {
                        InsurerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.InsurerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Insurers");
        }
    }
}
