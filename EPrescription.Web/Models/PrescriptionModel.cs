using EPrescription.Entities;
using EPrescription.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPrescription.Web.Models
{
    public class PrescriptionModel
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public List<Clinic> Clinics { get; set; }
        public List<PatientMedicine> PatientMedicineList { get; set; } 
        private PatientService patientService;
        private MedicineService medicineService;
        private DosageCommentService dosageCommentService;
        private InvestigationService investigationService;
        private DoctorService doctorService;
        private ClinicService clinicService;
        public IEnumerable<Medicine> MedicineList { get; set; }
        public IEnumerable<DosageComment> DosageComments { get; set; }

        public IEnumerable<Investigation> InvestigationList { get; set; }
        public List<string> Investigations { get; set; }
       
        public PrescriptionModel()
        {
            patientService = new PatientService();
            medicineService = new MedicineService();
            dosageCommentService = new DosageCommentService();
            investigationService = new InvestigationService();
            doctorService = new DoctorService();
            clinicService = new ClinicService();
            MedicineList = medicineService.GetAllMedicines().OrderBy(s=>s.BrandName);
            DosageComments = dosageCommentService.GetAllDosageComment();
            InvestigationList = investigationService.GetAllInvestigations();
        }

        public void Add()
        {
           foreach(var patientMedicine in PatientMedicineList)
            {
                patientMedicine.PatientId = Patient.Id;
                patientService.AddPatientMedicine(patientMedicine);
            }
           foreach(var investigation in Investigations)
            {
                var newPatientInvestigation = new PatientInvestigation()
                {
                    PatientId = Patient.Id,
                    Investigation = investigation
                };
                patientService.AddPatientInvestigation(newPatientInvestigation);
            }
        }

        public PrescriptionModel(int patientId):this()
        {
            Patient = patientService.GetPatientById(patientId);
            Doctor = doctorService.GetFirstDefault();
            Clinics = clinicService.GetAllClinics().Take(2).ToList();
           
        }
    }
  

}