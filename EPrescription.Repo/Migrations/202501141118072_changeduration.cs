namespace EPrescription.Repo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeduration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientMedicines", "Duration", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientMedicines", "Duration", c => c.Int(nullable: false));
        }
    }
}
