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

        public bool IsProcedureNameExist(string procedureName, string initialProcedureName)
        {
            return procedureUnitOfWork.ProcedureRepository.IsProcedureNameExist(procedureName, initialProcedureName);
        }
    }
}
