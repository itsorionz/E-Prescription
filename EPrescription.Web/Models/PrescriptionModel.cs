using EPrescription.Common;
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
        public List<PatientInvestigation> PatientInvestigationList { get; set; }
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
        public List<int> InvestigationIds { get; set; }

        public PrescriptionModel()
        {
            patientService = new PatientService();
            medicineService = new MedicineService();
            dosageCommentService = new DosageCommentService();
            investigationService = new InvestigationService();
            doctorService = new DoctorService();
            clinicService = new ClinicService();
            MedicineList = medicineService.GetAllMedicines().OrderBy(s => s.BrandName);
            DosageComments = dosageCommentService.GetAllDosageComment();
            InvestigationList = investigationService.GetAllInvestigations();
        }

        public void AddOrUpdate()
        {
            var existingMedicines = patientService.GetPatientMedicines(Patient.Id);

            if (PatientMedicineList != null && PatientMedicineList.Any())
            {
                foreach (var patientMedicine in PatientMedicineList)
                {
                    patientMedicine.PatientId = Patient.Id;
                    patientMedicine.StatusFlag = (byte)EnumActiveDeative.Active;

                    var existingMedicine = existingMedicines
                        .FirstOrDefault(m => m.Medicine == patientMedicine.Medicine);

                    if (existingMedicine != null)
                    {
                        existingMedicine.Avaiablity = patientMedicine.Avaiablity;
                        existingMedicine.Dosage = patientMedicine.Dosage;
                        existingMedicine.DosageComment = patientMedicine.DosageComment;
                        existingMedicine.Duration = patientMedicine.Duration;
                        existingMedicine.DurationUnit = patientMedicine.DurationUnit;
                        patientService.UpdatePatientMedicine(existingMedicine);
                    }
                    else
                    {
                        patientService.AddPatientMedicine(patientMedicine);
                    }
                }

                var medicinesToDelete = existingMedicines.Where(m => !PatientMedicineList.Any(pm => pm.Medicine == m.Medicine)).ToList();

                foreach (var medicine in medicinesToDelete)
                {
                    medicine.StatusFlag = (byte)EnumActiveDeative.Inactive;
                    patientService.UpdatePatientMedicine(medicine);
                }
            }
            else
            {
                foreach (var medicine in existingMedicines)
                {
                    medicine.StatusFlag = (byte)EnumActiveDeative.Inactive;
                    patientService.UpdatePatientMedicine(medicine);
                }
            }


            //var existingInvestigations = patientService.GetPatientInvestigationsByPatientId(Patient.Id);

            //if (Investigations != null && Investigations.Any())
            //{
            //    foreach (var investigation in Investigations)
            //    {
            //        var existingInvestigation = existingInvestigations
            //            .FirstOrDefault(i => i.Investigation == investigation);

            //        if (existingInvestigation != null)
            //        {
            //            // Update existing investigation
            //            existingInvestigation.StatusFlag = (byte)EnumActiveDeative.Active;
            //            patientService.UpdatePatientInvestigation(existingInvestigation);
            //        }
            //        else
            //        {
            //            // Add new investigation
            //            var newPatientInvestigation = new PatientInvestigation
            //            {
            //                PatientId = Patient.Id,
            //                StatusFlag = (byte)EnumActiveDeative.Active,
            //                Investigation = investigation
            //            };
            //            patientService.AddPatientInvestigation(newPatientInvestigation);
            //        }
            //    }

            //    // Delete investigations that are not in the passed list
            //    var investigationsToDelete = existingInvestigations
            //        .Where(i => !Investigations.Contains(i.Investigation))
            //        .ToList();

            //    foreach (var investigation in investigationsToDelete)
            //    {
            //        patientService.DeletePatientInvestigation(investigation);
            //    }
            //}
            //else
            //{
            //    foreach (var investigation in existingInvestigations)
            //    {
            //        patientService.DeletePatientInvestigation(investigation);
            //    }
            //}
        }


        public PrescriptionModel(int patientId) : this()
        {
            Patient = patientService.GetPatientById(patientId);
            Doctor = doctorService.GetFirstDefault();
            Clinics = clinicService.GetAllClinics().Take(2).ToList();
            PatientMedicineList = patientService.GetPatientMedicines(patientId);
            PatientInvestigationList = patientService.GetPatientInvestigation(patientId);
            InvestigationIds = patientService.GetPatientInvestigationByPatientId(patientId);
        }

    }
}