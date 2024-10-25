using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class DosageCommentUnitOfWork : IDisposable
    {
        private DosageCommentRepository dosageCommentRepository;
        private EPrescriptionDbContext _context;

        public DosageCommentUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            this.dosageCommentRepository = new DosageCommentRepository(_context);
          
        }
        public void Save()
        {
            _context.SaveChanges();

        }
        public DosageCommentRepository DosageCommentRepository
        {
            get
            {
                return dosageCommentRepository;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
