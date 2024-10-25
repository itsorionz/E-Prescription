using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class PatientUnitOfWork : IDisposable
    {
        private PatientRepository patientRepository;
        private EPrescriptionDbContext _context;
        private PatientComplaintRepository patientComplaintRepository;
        private PatientDiseaseRepository patientDiseaseRepository;
        private PatientMedicineRepository patientMedicineRepository;
        private PatientProcedureRepository patientProcedureRepository;
        private PatientInvestigationRepository patientInvestigationRepository;
        
        public PatientUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            patientRepository = new PatientRepository(_context);
            patientProcedureRepository = new PatientProcedureRepository(_context);
            patientMedicineRepository = new PatientMedicineRepository(_context);
            patientDiseaseRepository = new PatientDiseaseRepository(_context);
            patientComplaintRepository = new PatientComplaintRepository(_context);
            patientInvestigationRepository = new PatientInvestigationRepository(_context);
        }
        public PatientRepository PatientRepository
        {
            get
            {
                return patientRepository;
            }
        }
        public PatientComplaintRepository PatientComplaintRepository => patientComplaintRepository;
        public PatientDiseaseRepository PatientDiseaseRepository => patientDiseaseRepository;
        public PatientMedicineRepository PatientMedicineRepository => patientMedicineRepository;
        public PatientProcedureRepository PatientProcedureRepository => patientProcedureRepository;
        public PatientInvestigationRepository PatientInvestigationRepository => patientInvestigationRepository;

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
