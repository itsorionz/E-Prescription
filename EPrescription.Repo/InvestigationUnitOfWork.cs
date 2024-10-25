using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class InvestigationUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context;
        private InvestigationRepository investigationRepository;
        public InvestigationUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            investigationRepository = new InvestigationRepository(_context);
        }
        public InvestigationRepository InvestigationRepository
        {
            get
            {
                return investigationRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.SaveChanges();
        }
    }
}
