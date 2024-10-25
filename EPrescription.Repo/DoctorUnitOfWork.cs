using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class DoctorUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context;
        private DoctorRepository _doctorRepository;

        public DoctorUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            _doctorRepository = new DoctorRepository(_context);
        }
        public DoctorRepository DoctorRepository => _doctorRepository;

        public void Save()
        {
            _context.SaveChanges();
        }

      

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
