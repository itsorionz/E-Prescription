using EPrescription.Common;
using EPrescription.Entities;
using EPrescription.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPrescription.Web.Models
{
    public class PatientModel:Patient
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
        public IEnumerable<Patient> GetAllPatients()
        {
            return patientService.GetAllPatients();
        }
        public void Add()
        {
            base.CreatedAt = DateTime.Now;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumPatientStatus.History_Collected;
            base.PatientComplaints = PrescriptionComplaintList.Where(s => s.Complaint!=null).ToList();
            base.PatientProcedures = PrescriptionProcedureList.Where(s => s.Procedure != null).ToList();
            base.PatientDiseases = PrescriptionDiseaseList.Where(s => s.Disease != null).ToList();
            int patientId=  patientService.AddPatient(this);
            
        }
        public IPagedList<Patient> GetAllIPagedList(int page, int pageSize, string name)
        {
            return patientService.GetAllIPagedList(page,pageSize, name);
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