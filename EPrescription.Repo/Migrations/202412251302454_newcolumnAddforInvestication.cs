namespace EPrescription.Repo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcolumnAddforInvestication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientInvestigations", "InvestigationId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientInvestigations", "InvestigationId");
        }
    }
}
