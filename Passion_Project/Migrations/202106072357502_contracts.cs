namespace Passion_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contracts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OwnerID = c.Int(nullable: false),
                        PolicyID = c.Int(nullable: false),
                        InsurerID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Insurers", t => t.InsurerID, cascadeDelete: true)
                .ForeignKey("dbo.Owners", t => t.OwnerID, cascadeDelete: true)
                .ForeignKey("dbo.Policies", t => t.PolicyID, cascadeDelete: true)
                .Index(t => t.OwnerID)
                .Index(t => t.PolicyID)
                .Index(t => t.InsurerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "PolicyID", "dbo.Policies");
            DropForeignKey("dbo.Contracts", "OwnerID", "dbo.Owners");
            DropForeignKey("dbo.Contracts", "InsurerID", "dbo.Insurers");
            DropIndex("dbo.Contracts", new[] { "InsurerID" });
            DropIndex("dbo.Contracts", new[] { "PolicyID" });
            DropIndex("dbo.Contracts", new[] { "OwnerID" });
            DropTable("dbo.Contracts");
        }
    }
}
