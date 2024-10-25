using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class DiseaseUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context;
        private DiseaseRepository diseaseRepository;
        public DiseaseUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            diseaseRepository = new DiseaseRepository(_context);
        }
        public DiseaseRepository DiseaseRepository
        {
            get
            {
                return diseaseRepository;
            }
        }
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
