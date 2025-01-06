using EPrescription.Common;
using EPrescription.Entities;
using EPrescription.Repo;
using EPrescription.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EPrescription.Services
{ 
    public class PatientService
    {
        private PatientUnitOfWork patientUnitOfWork;
        private EPrescriptionDbContext _context;
        
        public PatientService()
        {
            _context = new EPrescriptionDbContext();
            patientUnitOfWork = new PatientUnitOfWork(_context);
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return patientUnitOfWork.PatientRepository.GetAll();
        }
        public Patient GetPatientById(int patientId)
        {
            return patientUnitOfWork.PatientRepository.GetById(patientId);
        }
        public IPagedList<Patient> GetAllIPagedList(int page, int pageSize, string name)
        {
            return patientUnitOfWork.PatientRepository.GetAllIPagedList(page,  pageSize,  name);
        }
        public int AddPatient(Patient patient)
        {
            var newPatient = new Patient()
            {   PatientNo = GetPatientNo(),
                Name = patient.Name,
                Address = patient.Address,
                Age = patient.Age,
                BirthDate = patient.BirthDate,
                Gender = patient.Gender,
                PhoneNo = patient.PhoneNo,
                StatusFlag = patient.StatusFlag,
                CreatedAt = patient.CreatedAt,
                CreatedBy = patient.CreatedBy,
                GeneralDetail = patient.GeneralDetail,
                PatientDiseases = patient.PatientDiseases,
                PatientComplaints = patient.PatientComplaints,
                PatientProcedures = patient.PatientProcedures
            };
            patientUnitOfWork.PatientRepository.Add(newPatient);
            patientUnitOfWork.Save();
            return newPatient.Id;
        }
        private string GetPatientNo()
        {
            return string.Format("{0:ddMMyy}",DateTime.Now)+ (patientUnitOfWork.PatientRepository.GetCount(DateTime.Now)+1).ToString("00");
        }
        public void AddPatientInvestigation(PatientInvestigation patientInvestigation)
        {
            var newPatientInvestigation = new PatientInvestigation()
            {
                PatientId = patientInvestigation.PatientId,
                Investigation = patientInvestigation.Investigation,
                InvestigationId = patientInvestigation.InvestigationId,
                StatusFlag = (byte)EnumActiveDeative.Active
            };
            patientUnitOfWork.PatientInvestigationRepository.Add(newPatientInvestigation);
            patientUnitOfWork.Save();
        }
        public void AddPatientMedicine(PatientMedicine patientMedicine)
        {
            var newPatientMedicine = new PatientMedicine()
            {
                PatientId = patientMedicine.PatientId,
                Medicine = patientMedicine.Medicine,
                Avaiablity = patientMedicine.Avaiablity,
                Dosage = patientMedicine.Dosage,
                DosageComment = patientMedicine.DosageComment,
                Duration = patientMedicine.Duration,
                DurationUnit = patientMedicine.DurationUnit,
                StatusFlag = (byte)EnumActiveDeative.Active
            };
            patientUnitOfWork.PatientMedicineRepository.Add(patientMedicine);
            patientUnitOfWork.Save(); 
        }
        public List<PatientMedicine> GetPatientMedicines(int patientId)
        {
            var medicines = patientUnitOfWork.PatientMedicineRepository.FindAll(p => p.PatientId == patientId && p.StatusFlag == (byte)EnumActiveDeative.Active).ToList();
            if (!medicines.Any()) 
            {
                medicines.Add(new PatientMedicine
                {
                    Medicine = string.Empty
                });
            }
            return medicines;
        }
        public List<PatientInvestigation> GetPatientInvestigation(int patientId)
        {
            return patientUnitOfWork.PatientInvestigationRepository.FindAll(p => p.PatientId == patientId && p.StatusFlag == (byte)EnumActiveDeative.Active).ToList();
        }
        public List<int> GetPatientInvestigationByPatientId(int patientId)
        {
            return patientUnitOfWork.PatientInvestigationRepository.FindAll(p => p.PatientId == patientId && p.StatusFlag == (byte)EnumActiveDeative.Active).Select(p => p.InvestigationId).ToList();
        }
        public PatientMedicine GetPatientMedicine(int patientId, string medicineName)
        {
            return patientUnitOfWork.PatientMedicineRepository.FindAll(p => p.PatientId == patientId && p.Medicine == medicineName && p.StatusFlag == (byte)EnumActiveDeative.Active).FirstOrDefault();
        }
        public void UpdatePatientMedicine(PatientMedicine patientMedicine)
        {
            patientUnitOfWork.PatientMedicineRepository.Update(patientMedicine);
            patientUnitOfWork.Save();
        }
        public PatientInvestigation GetPatientInvestigation(int patientId, string investigation)
        {
            return patientUnitOfWork.PatientInvestigationRepository.FindAll(p => p.PatientId == patientId && p.Investigation == investigation && p.StatusFlag == (byte)EnumActiveDeative.Active).FirstOrDefault();
        }
        public void UpdatePatientInvestigation(PatientInvestigation patientInvestigation)
        {
            patientUnitOfWork.PatientInvestigationRepository.Update(patientInvestigation);
            patientUnitOfWork.Save();
        }
        public List<PatientComplaint> GetComplaintsByPatientId(int patientId)
        {
            var patientComplaintList = patientUnitOfWork.PatientComplaintRepository
                .GetAll()
                .Where(rel => rel.PatientId == patientId && rel.StatusFlag == (byte)EnumActiveDeative.Active)
                .ToList();
            return patientComplaintList;
        }
        public List<PatientDisease> GetDiseasesByPatientId(int patientId)
        {
            var patientDiseaseList = patientUnitOfWork.PatientDiseaseRepository
                .GetAll()
                .Where(rel => rel.PatientId == patientId && rel.StatusFlag == (byte)EnumActiveDeative.Active)
                .ToList();
            return patientDiseaseList;
        }
        public List<PatientProcedure> GetProceduresByPatientId(int patientId)
        {
            var patientProcedureList = patientUnitOfWork.PatientProcedureRepository
                .GetAll()
                .Where(rel => rel.PatientId == patientId && rel.StatusFlag == (byte)EnumActiveDeative.Active)
                .ToList();
            return patientProcedureList;
        }
        public void Edit(Patient model)
        {
            var patient = patientUnitOfWork.PatientRepository.GetById(model.Id);
            if (patient != null)
            {
                patient.UpdatedAt = DateTime.Now;
                patient.UpdatedBy = model.UpdatedBy;
                patient.StatusFlag = (byte)EnumActiveDeative.Active;
                patient.Name = model.Name;
                patient.Age = model.Age;
                patient.BirthDate = model.BirthDate;
                patient.Address = model.Address;
                patient.PhoneNo = model.PhoneNo;
                patient.Gender = model.Gender;
                patient.Comments = model.Comments;
                patient.GeneralDetailId = model.GeneralDetailId;
                patient.GeneralDetail = model.GeneralDetail;

                SyncComplaints(patient, model.PatientComplaints ?? new List<PatientComplaint>());
                SyncDiseases(patient, model.PatientDiseases ?? new List<PatientDisease>());
                SyncProcedures(patient, model.PatientProcedures ?? new List<PatientProcedure>());

                patientUnitOfWork.PatientRepository.Update(patient);
                patientUnitOfWork.Save();
            }
        }
        private void SyncComplaints(Patient patient, IEnumerable<PatientComplaint> updatedComplaints)
        {
            var existingComplaints = patient.PatientComplaints?.ToList() ?? new List<PatientComplaint>();
            foreach (var complaint in existingComplaints.ToList())
            {
                if (!updatedComplaints.Any(c => c.Complaint == complaint.Complaint && c.StatusFlag == (byte)EnumActiveDeative.Active))
                {
                    patientUnitOfWork.PatientComplaintRepository.DeleteById(complaint.Id);
                }
            }
            foreach (var complaint in updatedComplaints)
            {
                var existingComplaint = existingComplaints.FirstOrDefault(c => c.Complaint == complaint.Complaint);
                if (existingComplaint != null)
                {
                    existingComplaint.Complaint = complaint.Complaint;
                    existingComplaint.Remarks = complaint.Remarks;
                    if (existingComplaint.StatusFlag == (byte)EnumActiveDeative.Inactive)
                    {
                        existingComplaint.StatusFlag = (byte)EnumActiveDeative.Active;
                    }
                    patientUnitOfWork.PatientComplaintRepository.Update(existingComplaint);
                }
                else
                {
                    complaint.PatientId = patient.Id;
                    patientUnitOfWork.PatientComplaintRepository.Add(complaint);
                }
            }
        }
        private void SyncDiseases(Patient patient, IEnumerable<PatientDisease> updatedDiseases)
        {
            var existingDiseases = patient.PatientDiseases?.ToList() ?? new List<PatientDisease>();
            foreach (var disease in existingDiseases.ToList())
            {
                if (!updatedDiseases.Any(d => d.Disease == disease.Disease && d.StatusFlag == (byte)EnumActiveDeative.Active))
                {
                    patientUnitOfWork.PatientDiseaseRepository.DeleteById(disease.Id);
                }
            }
            foreach (var disease in updatedDiseases)
            {
                var existingDisease = existingDiseases.FirstOrDefault(d => d.Disease == disease.Disease);
                if (existingDisease != null)
                {
                    existingDisease.Disease = disease.Disease;
                    existingDisease.Remarks = disease.Remarks;
                    if (existingDisease.StatusFlag == (byte)EnumActiveDeative.Inactive)
                    {
                        existingDisease.StatusFlag = (byte)EnumActiveDeative.Active;
                    }
                    patientUnitOfWork.PatientDiseaseRepository.Update(existingDisease);
                }
                else
                {
                    disease.PatientId = patient.Id;
                    patientUnitOfWork.PatientDiseaseRepository.Add(disease);
                }
            }
            foreach (var disease in updatedDiseases)
            {
                var deletedDisease = existingDiseases.FirstOrDefault(d => d.Disease == disease.Disease);
                if (deletedDisease == null)
                {
                    disease.PatientId = patient.Id;
                    patientUnitOfWork.PatientDiseaseRepository.Add(disease);
                }
            }
        }
        private void SyncProcedures(Patient patient, IEnumerable<PatientProcedure> updatedProcedures)
        {
            var existingProcedures = patient.PatientProcedures?.ToList() ?? new List<PatientProcedure>();

            foreach (var procedure in existingProcedures.ToList())
            {
                if (!updatedProcedures.Any(p => p.Procedure == procedure.Procedure && p.StatusFlag == (byte)EnumActiveDeative.Active))
                {
                    patientUnitOfWork.PatientProcedureRepository.DeleteById(procedure.Id);
                }
            }
            foreach (var procedure in updatedProcedures)
            {
                var existingProcedure = existingProcedures.FirstOrDefault(p => p.Procedure == procedure.Procedure);
                if (existingProcedure != null)
                {
                    existingProcedure.Procedure = procedure.Procedure;
                    existingProcedure.Remarks = procedure.Remarks;
                    if (existingProcedure.StatusFlag == (byte)EnumActiveDeative.Inactive)
                    {
                        existingProcedure.StatusFlag = (byte)EnumActiveDeative.Active;
                    }
                    patientUnitOfWork.PatientProcedureRepository.Update(existingProcedure);
                }
                else
                {
                    procedure.PatientId = patient.Id;
                    patientUnitOfWork.PatientProcedureRepository.Add(procedure);
                }
            }
            foreach (var procedure in updatedProcedures)
            {
                var deletedProcedure = existingProcedures.FirstOrDefault(p => p.Procedure == procedure.Procedure);
                if (deletedProcedure == null)
                {
                    procedure.PatientId = patient.Id;
                    patientUnitOfWork.PatientProcedureRepository.Add(procedure);
                }
            }
        }
        public void Inactive(Patient patient)
        {
            var patientEntry = GetPatientById(patient.Id);
            if (patientEntry != null)
            {
                patientEntry.UpdatedAt = patient.UpdatedAt;
                patientEntry.UpdatedBy = patient.UpdatedBy;
                patientUnitOfWork.PatientRepository.DeleteByItem(patientEntry);
                patientUnitOfWork.Save();
            }
        }
    }
}
