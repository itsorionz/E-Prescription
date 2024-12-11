using EPrescription.Entities;
using EPrescription.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Services
{
   public class ProcedureService
    {
        private EPrescriptionDbContext _context;
        private ProcedureUnitOfWork procedureUnitOfWork;
        public ProcedureService()
        {
            _context = new EPrescriptionDbContext();
            procedureUnitOfWork = new ProcedureUnitOfWork(_context);
        }

        public IEnumerable<Procedure> GetAllProcedures()
        {
            return procedureUnitOfWork.ProcedureRepository.GetAll();
        }
        public Procedure GetProceduretById(int id)
        {
            return procedureUnitOfWork.ProcedureRepository.GetById(id);
        }

        public void Add(Procedure procedure)
        {
            var newProcedure = new Procedure()
            {
                ProcedureName = procedure.ProcedureName,
                StatusFlag = procedure.StatusFlag,
                CreatedBy = procedure.CreatedBy,
            };
            procedureUnitOfWork.ProcedureRepository.Add(newProcedure);
            procedureUnitOfWork.Save();
        }

        public bool IsProcedureNameExist(string procedureName, string InitialProcedureName)
        {
            return procedureUnitOfWork.ProcedureRepository.IsProcedureNameExist(procedureName, InitialProcedureName);
        }

        public void Edit(Procedure procedure)
        {
            var procedureEntry = GetProceduretById(procedure.Id);
            if (procedureEntry != null)
            {
                procedureEntry.ProcedureName = procedure.ProcedureName;
                procedureEntry.UpdatedAt = procedure.UpdatedAt;
                procedureEntry.UpdatedBy = procedure.UpdatedBy;
                procedureUnitOfWork.ProcedureRepository.Update(procedureEntry);
                procedureUnitOfWork.Save();
            }
        }

        public void Inactive(Procedure procedure)
        {
            var procedureEntry = GetProceduretById(procedure.Id);
            if (procedureEntry != null)
            {
                procedureEntry.UpdatedAt = procedure.UpdatedAt;
                procedureEntry.UpdatedBy = procedure.UpdatedBy;
                procedureUnitOfWork.ProcedureRepository.DeleteByItem(procedureEntry);
                procedureUnitOfWork.Save();
            }
        }
    }
}
