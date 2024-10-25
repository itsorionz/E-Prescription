using EPrescription.Entities;
using EPrescription.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Services
{
   public class DosageCommentService
    {
        private DosageCommentUnitOfWork dosageCommentUnitOfWork;
        private EPrescriptionDbContext _context;

        public DosageCommentService()
        {
            _context = new EPrescriptionDbContext();
            dosageCommentUnitOfWork = new DosageCommentUnitOfWork(_context);
        }
        public IEnumerable<DosageComment> GetAllDosageComment()
        {
            return dosageCommentUnitOfWork.DosageCommentRepository.GetAll();
        }
    }
}
