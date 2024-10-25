using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class ClinicUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context;
        private ClinicRepository _clinicRepository;

        public ClinicUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            _clinicRepository = new ClinicRepository(_context);
        }
        public void Save() => _context.SaveChanges();
        public ClinicRepository ClinicRepository => _clinicRepository;
        public void Dispose() => _context.Dispose();
    }
}
