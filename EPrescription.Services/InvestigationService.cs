using EPrescription.Entities;
using EPrescription.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Services
{
   public class InvestigationService
    {
        private EPrescriptionDbContext _context;
        private InvestigationUnitOfWork investigationUnitOfWork;
        public InvestigationService()
        {
            _context = new EPrescriptionDbContext();
            investigationUnitOfWork = new InvestigationUnitOfWork(_context);
        }

        public IEnumerable<Investigation> GetAllInvestigations()
        {
            return investigationUnitOfWork.InvestigationRepository.GetAll();
        }

        public void Add(Investigation investigation)
        {
            var newInvestigation = new Investigation()
            {
                InvestigationName = investigation.InvestigationName,
                StatusFlag = investigation.StatusFlag,
                CreatedBy = investigation.CreatedBy
            };
            investigationUnitOfWork.InvestigationRepository.Add(newInvestigation);
            investigationUnitOfWork.Save();
        }

        public bool IsInvestigationNameExist(string investigationName, string initialInvestigationName)
        {
            return investigationUnitOfWork.InvestigationRepository.IsInvestigationNameExist(investigationName, initialInvestigationName);
        }
    }
}
