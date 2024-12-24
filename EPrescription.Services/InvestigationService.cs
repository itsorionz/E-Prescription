using EPrescription.Common;
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
            return investigationUnitOfWork.InvestigationRepository.GetAll().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        }

        public Investigation GetInvestigationById(int id)
        {
            return investigationUnitOfWork.InvestigationRepository.GetById(id);
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

        public void Edit(Investigation investigation)
        {
            var investigationEntry = GetInvestigationById(investigation.Id);
            if (investigationEntry != null)
            {
                investigationEntry.InvestigationName = investigation.InvestigationName;
                investigationEntry.UpdatedAt = investigation.UpdatedAt;
                investigationEntry.UpdatedBy = investigation.UpdatedBy;
                investigationUnitOfWork.InvestigationRepository.Update(investigationEntry);
                investigationUnitOfWork.Save();
            }
        }
        public void Inactive(Investigation investigation)
        {
            var investigationEntry = GetInvestigationById(investigation.Id);
            if (investigationEntry != null)
            {
                investigationEntry.UpdatedAt = investigation.UpdatedAt;
                investigationEntry.UpdatedBy = investigation.UpdatedBy;
                investigationUnitOfWork.InvestigationRepository.DeleteByItem(investigationEntry);
                investigationUnitOfWork.Save();
            }
        }
    }
}
