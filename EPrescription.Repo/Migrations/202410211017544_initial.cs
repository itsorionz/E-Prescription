namespace EPrescription.Repo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        AuditLogId = c.Guid(nullable: false),
                        EventType = c.String(),
                        TableName = c.String(nullable: false),
                        PrimaryKeyName = c.String(),
                        PrimaryKeyValue = c.String(),
                        ColumnName = c.String(),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        CreatedUser = c.Int(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.AuditLogId)
                .ForeignKey("dbo.Users", t => t.CreatedUser)
                .Index(t => t.CreatedUser);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Email = c.String(),
                        MobileNo = c.String(),
                        Address = c.String(),
                        Gender = c.String(),
                        SupUser = c.Boolean(nullable: false),
                        RoleId = c.Int(),
                        ImageFile = c.String(),
                        JoiningDate = c.DateTime(),
                        Password = c.String(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.RoleTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        Task = c.String(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Clinics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Timings = c.String(),
                        OffDays = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComplaintType = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiseaseName = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Degree = c.String(),
                        Specialty = c.String(),
                        Institute = c.String(),
                        RegNo = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.DosageComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentType = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.DosageTypeMedicineRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DosageTypeId = c.Int(),
                        MedicineId = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DosageTypes", t => t.DosageTypeId)
                .ForeignKey("dbo.Medicines", t => t.MedicineId)
                .Index(t => t.DosageTypeId)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.DosageTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicineManufacturerId = c.Int(),
                        BrandName = c.String(),
                        UseFor = c.String(),
                        Dar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.MedicineManufacturers", t => t.MedicineManufacturerId)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.MedicineManufacturerId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.GenericNameMedicineRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenericTypeId = c.Int(),
                        MedicineId = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GenericNames", t => t.GenericTypeId)
                .ForeignKey("dbo.Medicines", t => t.MedicineId)
                .Index(t => t.GenericTypeId)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.GenericNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.MedicineManufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Address = c.String(),
                        ContactNumber = c.String(),
                        Email = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.StrengthMedicineRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StrengthId = c.Int(),
                        MedicineId = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicines", t => t.MedicineId)
                .ForeignKey("dbo.Strengths", t => t.StrengthId)
                .Index(t => t.StrengthId)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.Strengths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StrengthName = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.GeneralDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Height = c.Single(),
                        Weigth = c.Single(),
                        Temp = c.Int(),
                        Pulse = c.Int(),
                        BP = c.Int(),
                        Smoker = c.Boolean(nullable: false),
                        Allergy = c.Boolean(nullable: false),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Investigations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvestigationName = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.PatientComplaints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        Complaint = c.String(),
                        Remarks = c.String(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientNo = c.String(),
                        Name = c.String(nullable: false),
                        Age = c.Int(),
                        BirthDate = c.DateTime(),
                        Address = c.String(),
                        PhoneNo = c.String(),
                        Gender = c.String(),
                        Comments = c.String(),
                        GeneralDetailId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.GeneralDetails", t => t.GeneralDetailId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.GeneralDetailId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.PatientDiseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        Disease = c.String(),
                        Remarks = c.String(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.PatientInvestigations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        Investigation = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.PatientId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.PatientMedicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        Medicine = c.String(),
                        GenericName = c.String(),
                        Dosage = c.String(),
                        DosageComment = c.String(),
                        Duration = c.Int(nullable: false),
                        DurationUnit = c.String(),
                        Avaiablity = c.String(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.PatientProcedures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        Procedure = c.String(),
                        Remarks = c.String(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Procedures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcedureName = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedBy = c.Int(),
                        StatusFlag = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Procedures", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Procedures", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Patients", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.PatientProcedures", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.PatientMedicines", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.PatientInvestigations", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.PatientInvestigations", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.PatientInvestigations", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.PatientDiseases", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.PatientComplaints", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "GeneralDetailId", "dbo.GeneralDetails");
            DropForeignKey("dbo.Patients", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Investigations", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Investigations", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Medicines", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.StrengthMedicineRelations", "StrengthId", "dbo.Strengths");
            DropForeignKey("dbo.Strengths", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Strengths", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.StrengthMedicineRelations", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.Medicines", "MedicineManufacturerId", "dbo.MedicineManufacturers");
            DropForeignKey("dbo.MedicineManufacturers", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.MedicineManufacturers", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.GenericNameMedicineRelations", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.GenericNameMedicineRelations", "GenericTypeId", "dbo.GenericNames");
            DropForeignKey("dbo.GenericNames", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.GenericNames", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.DosageTypeMedicineRelations", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.Medicines", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.DosageTypeMedicineRelations", "DosageTypeId", "dbo.DosageTypes");
            DropForeignKey("dbo.DosageTypes", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.DosageTypes", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.DosageComments", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.DosageComments", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Doctors", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Doctors", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Diseases", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Diseases", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Complaints", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Complaints", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Clinics", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Clinics", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.AuditLogs", "CreatedUser", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Roles", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.RoleTasks", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Roles", "CreatedBy", "dbo.Users");
            DropIndex("dbo.Procedures", new[] { "UpdatedBy" });
            DropIndex("dbo.Procedures", new[] { "CreatedBy" });
            DropIndex("dbo.PatientProcedures", new[] { "PatientId" });
            DropIndex("dbo.PatientMedicines", new[] { "PatientId" });
            DropIndex("dbo.PatientInvestigations", new[] { "UpdatedBy" });
            DropIndex("dbo.PatientInvestigations", new[] { "CreatedBy" });
            DropIndex("dbo.PatientInvestigations", new[] { "PatientId" });
            DropIndex("dbo.PatientDiseases", new[] { "PatientId" });
            DropIndex("dbo.Patients", new[] { "UpdatedBy" });
            DropIndex("dbo.Patients", new[] { "CreatedBy" });
            DropIndex("dbo.Patients", new[] { "GeneralDetailId" });
            DropIndex("dbo.PatientComplaints", new[] { "PatientId" });
            DropIndex("dbo.Investigations", new[] { "UpdatedBy" });
            DropIndex("dbo.Investigations", new[] { "CreatedBy" });
            DropIndex("dbo.Strengths", new[] { "UpdatedBy" });
            DropIndex("dbo.Strengths", new[] { "CreatedBy" });
            DropIndex("dbo.StrengthMedicineRelations", new[] { "MedicineId" });
            DropIndex("dbo.StrengthMedicineRelations", new[] { "StrengthId" });
            DropIndex("dbo.MedicineManufacturers", new[] { "UpdatedBy" });
            DropIndex("dbo.MedicineManufacturers", new[] { "CreatedBy" });
            DropIndex("dbo.GenericNames", new[] { "UpdatedBy" });
            DropIndex("dbo.GenericNames", new[] { "CreatedBy" });
            DropIndex("dbo.GenericNameMedicineRelations", new[] { "MedicineId" });
            DropIndex("dbo.GenericNameMedicineRelations", new[] { "GenericTypeId" });
            DropIndex("dbo.Medicines", new[] { "UpdatedBy" });
            DropIndex("dbo.Medicines", new[] { "CreatedBy" });
            DropIndex("dbo.Medicines", new[] { "MedicineManufacturerId" });
            DropIndex("dbo.DosageTypes", new[] { "UpdatedBy" });
            DropIndex("dbo.DosageTypes", new[] { "CreatedBy" });
            DropIndex("dbo.DosageTypeMedicineRelations", new[] { "MedicineId" });
            DropIndex("dbo.DosageTypeMedicineRelations", new[] { "DosageTypeId" });
            DropIndex("dbo.DosageComments", new[] { "UpdatedBy" });
            DropIndex("dbo.DosageComments", new[] { "CreatedBy" });
            DropIndex("dbo.Doctors", new[] { "UpdatedBy" });
            DropIndex("dbo.Doctors", new[] { "CreatedBy" });
            DropIndex("dbo.Diseases", new[] { "UpdatedBy" });
            DropIndex("dbo.Diseases", new[] { "CreatedBy" });
            DropIndex("dbo.Complaints", new[] { "UpdatedBy" });
            DropIndex("dbo.Complaints", new[] { "CreatedBy" });
            DropIndex("dbo.Clinics", new[] { "UpdatedBy" });
            DropIndex("dbo.Clinics", new[] { "CreatedBy" });
            DropIndex("dbo.RoleTasks", new[] { "RoleId" });
            DropIndex("dbo.Roles", new[] { "UpdatedBy" });
            DropIndex("dbo.Roles", new[] { "CreatedBy" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.AuditLogs", new[] { "CreatedUser" });
            DropTable("dbo.Procedures");
            DropTable("dbo.PatientProcedures");
            DropTable("dbo.PatientMedicines");
            DropTable("dbo.PatientInvestigations");
            DropTable("dbo.PatientDiseases");
            DropTable("dbo.Patients");
            DropTable("dbo.PatientComplaints");
            DropTable("dbo.Investigations");
            DropTable("dbo.GeneralDetails");
            DropTable("dbo.Strengths");
            DropTable("dbo.StrengthMedicineRelations");
            DropTable("dbo.MedicineManufacturers");
            DropTable("dbo.GenericNames");
            DropTable("dbo.GenericNameMedicineRelations");
            DropTable("dbo.Medicines");
            DropTable("dbo.DosageTypes");
            DropTable("dbo.DosageTypeMedicineRelations");
            DropTable("dbo.DosageComments");
            DropTable("dbo.Doctors");
            DropTable("dbo.Diseases");
            DropTable("dbo.Complaints");
            DropTable("dbo.Clinics");
            DropTable("dbo.RoleTasks");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.AuditLogs");
        }
    }
}
