using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class ProcedureUnitOfWork : IDisposable
    {
        private ProcedureRepository procedureRepository;
        private EPrescriptionDbContext _context;
        public ProcedureUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            procedureRepository = new ProcedureRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public ProcedureRepository ProcedureRepository
        {
            get
            {
                return procedureRepository;
            }
        }
        public void Dispose()
        {
            _context.Dispose();   
        }
    }
}
