using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class GenericNameUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context;
        private GenericNameRepository _genericNameRepository;
        public GenericNameUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            _genericNameRepository = new GenericNameRepository(_context);
        }
        public GenericNameRepository GenericNameRepository
        {
            get { return _genericNameRepository; }
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
