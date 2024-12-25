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

        public PrescriptionModel(int patientId) : this()
        {
            Patient = patientService.GetPatientById(patientId);
            Doctor = doctorService.GetFirstDefault();
            Clinics = clinicService.GetAllClinics().Take(2).ToList();
            PatientMedicineList = patientService.GetPatientMedicines(patientId);
            PatientInvestigationList = patientService.GetPatientInvestigation(patientId);
            InvestigationIds = patientService.GetPatientInvestigationByPatientId(patientId);
        }

        public void AddOrUpdate()
        {
            UpdateMedicines();
            UpdateInvestigations();
        }

        private void UpdateMedicines()
        {
            var existingMedicines = patientService.GetPatientMedicines(Patient.Id);
            if (PatientMedicineList != null && PatientMedicineList.Any())
            {
                foreach (var patientMedicine in PatientMedicineList)
                {
                    patientMedicine.PatientId = Patient.Id;
                    patientMedicine.StatusFlag = (byte)EnumActiveDeative.Active;
                    var existingMedicine = existingMedicines.FirstOrDefault(m => m.Medicine == patientMedicine.Medicine);
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
        }

        private void UpdateInvestigations()
        {
            var existingInvestigations = patientService.GetPatientInvestigation(Patient.Id);
            if (InvestigationIds != null && InvestigationIds.Any())
            {
                foreach (var investigationId in InvestigationIds)
                {
                    var existingInvestigation = existingInvestigations.FirstOrDefault(i => i.Id == investigationId);
                    if (existingInvestigation != null)
                    {
                        existingInvestigation.StatusFlag = (byte)EnumActiveDeative.Active;
                        patientService.UpdatePatientInvestigation(existingInvestigation);
                    }
                    else
                    {
                        var investigationDetails = investigationService.GetInvestigationById(investigationId);
                        if (investigationDetails != null)
                        {
                            var newPatientInvestigation = new PatientInvestigation
                            {
                                PatientId = Patient.Id,
                                Id = investigationId,
                                Investigation = investigationDetails.InvestigationName,
                                StatusFlag = (byte)EnumActiveDeative.Active
                            };
                            patientService.AddPatientInvestigation(newPatientInvestigation);
                        }
                    }
                }
                var investigationsToDeactivate = existingInvestigations.Where(i => !InvestigationIds.Contains(i.Id)).ToList();
                foreach (var investigation in investigationsToDeactivate)
                {
                    investigation.StatusFlag = (byte)EnumActiveDeative.Inactive;
                    patientService.UpdatePatientInvestigation(investigation);
                }
            }
            else
            {
                foreach (var investigation in existingInvestigations)
                {
                    investigation.StatusFlag = (byte)EnumActiveDeative.Inactive;
                    patientService.UpdatePatientInvestigation(investigation);
                }
            }
        }
    }
}