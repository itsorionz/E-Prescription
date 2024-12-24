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
            {   PatientNo=GetPatientNo(),
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
                Investigation = patientInvestigation.Investigation 
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
                DurationUnit = patientMedicine.DurationUnit
            };
            patientUnitOfWork.PatientMedicineRepository.Add(patientMedicine);
            patientUnitOfWork.Save(); 
        }
        public List<PatientMedicine> GetPatientMedicines(int patientId)
        {
            var medicines = patientUnitOfWork.PatientMedicineRepository.FindAll(p => p.PatientId == patientId).ToList();
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
            return patientUnitOfWork.PatientInvestigationRepository.FindAll(p => p.PatientId == patientId).ToList();
        } 
    }
}
