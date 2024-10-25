using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class ComplaintUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context;
        private ComplaintRepository complaintRepository;
        public ComplaintUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            complaintRepository = new ComplaintRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public ComplaintRepository ComplaintRepository
        {
            get
            {
                return complaintRepository;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
