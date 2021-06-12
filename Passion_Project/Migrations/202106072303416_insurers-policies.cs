namespace Passion_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insurerspolicies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PolicyInsurers",
                c => new
                    {
                        Policy_PolicyID = c.Int(nullable: false),
                        Insurer_InsurerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Policy_PolicyID, t.Insurer_InsurerID })
                .ForeignKey("dbo.Policies", t => t.Policy_PolicyID, cascadeDelete: true)
                .ForeignKey("dbo.Insurers", t => t.Insurer_InsurerID, cascadeDelete: true)
                .Index(t => t.Policy_PolicyID)
                .Index(t => t.Insurer_InsurerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PolicyInsurers", "Insurer_InsurerID", "dbo.Insurers");
            DropForeignKey("dbo.PolicyInsurers", "Policy_PolicyID", "dbo.Policies");
            DropIndex("dbo.PolicyInsurers", new[] { "Insurer_InsurerID" });
            DropIndex("dbo.PolicyInsurers", new[] { "Policy_PolicyID" });
            DropTable("dbo.PolicyInsurers");
        }
    }
}
