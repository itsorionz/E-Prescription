using EPrescription.Common;
using EPrescription.Entities;
using EPrescription.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPrescription.Web.Models
{
    public class PatientModel : Patient
    {
        private PatientService patientService;
        private ProcedureService procedureService;
        private ComplaintService complaintService;
        private DiseaseService diseaseService;

        public IEnumerable<Procedure> ProcedureList { get; set; }
        public IEnumerable<Complaint> ComplaintList { get; set; }
        public IEnumerable<Disease> DiseaseList { get; set; }
        public List<PatientDisease> PrescriptionDiseaseList { get; set; }
        public List<PatientComplaint> PrescriptionComplaintList { get; set; }
        public List<PatientProcedure> PrescriptionProcedureList { get; set; }


        public PatientModel()
        {
            patientService = new PatientService();
            diseaseService = new DiseaseService();
            complaintService = new ComplaintService();
            procedureService = new ProcedureService();
            ProcedureList = procedureService.GetAllProcedures();
            ComplaintList = complaintService.GetAllComplaints();
            DiseaseList = diseaseService.GetAllDiseases();
        }
        public PatientModel(int id) : this()
        {
            var patientEntry = patientService.GetPatientById(id);
            {
                base.Id = patientEntry.Id;
                base.Name = patientEntry.Name;
                base.PhoneNo = patientEntry.PhoneNo;
                base.Age = patientEntry.Age;
                base.CreatedAt = patientEntry.CreatedAt;
                base.StatusFlag = patientEntry.StatusFlag;
                base.UpdatedAt = patientEntry.UpdatedAt;
                base.UpdatedBy = patientEntry.UpdatedBy;
                base.CreatedBy = patientEntry.CreatedBy;
                base.Gender = patientEntry.Gender;
                base.BirthDate = patientEntry.BirthDate;
                base.Address = patientEntry.Address;
                base.PatientComplaints = patientEntry.PatientComplaints;
                base.PatientDiseases = patientEntry.PatientDiseases;
                base.PatientMedicines = patientEntry.PatientMedicines;
                base.PatientProcedures = patientEntry.PatientProcedures;
                base.PatientInvestigations = patientEntry.PatientInvestigations;
                base.GeneralDetail = patientEntry.GeneralDetail;
                base.GeneralDetailId = patientEntry.GeneralDetailId;
                PrescriptionComplaintList = patientService.GetComplaintsByPatientId(id);
                PrescriptionDiseaseList = patientService.GetDiseasesByPatientId(id);
                PrescriptionProcedureList = patientService.GetProceduresByPatientId(id);
            }
        }
        
        public IEnumerable<Patient> GetAllPatients()
        {
            return patientService.GetAllPatients();
        }
        public void Add()
        {
            base.CreatedAt = DateTime.Now;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumPatientStatus.History_Collected;
            base.PatientComplaints = PrescriptionComplaintList.Where(s => s.Complaint != null).ToList();
            base.PatientProcedures = PrescriptionProcedureList.Where(s => s.Procedure != null).ToList();
            base.PatientDiseases = PrescriptionDiseaseList.Where(s => s.Disease != null).ToList();
            int patientId =  patientService.AddPatient(this);       
        }
        public IPagedList<Patient> GetAllIPagedList(int page, int pageSize, string name)
        {
            return patientService.GetAllIPagedList(page,pageSize, name);
        }
        public PatientModel Edit(int id)
        {
            var patient = patientService.GetPatientById(id);
            var patientComplaints = patientService.GetComplaintsByPatientId(id);
            var patientDiseases = patientService.GetDiseasesByPatientId(id);
            var patientProcedures = patientService.GetProceduresByPatientId(id);
            if (patient == null)
            {
                return null;
            }
            var patientModel = new PatientModel
            {
                Id = patient.Id,
                Name = patient.Name,
                Age = patient.Age,
                StatusFlag = patient.StatusFlag,
                CreatedBy = patient.CreatedBy,
                BirthDate = patient.BirthDate,
                Address = patient.Address,
                PhoneNo = patient.PhoneNo,
                Gender = patient.Gender,
                Comments = patient.Comments,
                GeneralDetailId = patient.GeneralDetailId,
                PatientComplaints = patientComplaints,
                PatientDiseases = patientDiseases,
                PatientMedicines = patient.PatientMedicines,
                PatientProcedures = patientProcedures,
                PatientInvestigations = patient.PatientInvestigations,
                GeneralDetail = patient.GeneralDetail,
                ComplaintList = ComplaintList,
                DiseaseList = DiseaseList,
                ProcedureList = ProcedureList,
                PrescriptionComplaintList = patientService.GetComplaintsByPatientId(id),
                PrescriptionDiseaseList = patientService.GetDiseasesByPatientId(id),
                PrescriptionProcedureList = patientService.GetProceduresByPatientId(id)
            };
            return patientModel;
        }
        public void Edit(PatientModel model)
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            base.GeneralDetail = model.GeneralDetail;
            base.PatientComplaints = model.PrescriptionComplaintList;
            base.PatientDiseases = model.PrescriptionDiseaseList;
            base.PatientProcedures = model.PrescriptionProcedureList;
            patientService.Edit(this);
        }

        public void Inactive()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Inactive;
            patientService.Inactive(this);
        }
    }
}