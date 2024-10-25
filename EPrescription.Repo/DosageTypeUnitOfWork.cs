using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class DosageTypeUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context;
        private DosageTypeRepository dosageTypeRepository;
        public DosageTypeUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            dosageTypeRepository = new DosageTypeRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public DosageTypeRepository DosageTypeRepository
        {
            get
            {
                return dosageTypeRepository;
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
