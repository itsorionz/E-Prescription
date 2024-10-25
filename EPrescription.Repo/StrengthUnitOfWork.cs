using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class StrengthUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context;
        private StrengthRepository strengthRepository;
        public StrengthUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            strengthRepository = new StrengthRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public StrengthRepository StrengthRepository
        {
            get
            {
                return strengthRepository;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
