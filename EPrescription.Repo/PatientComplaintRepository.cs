using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class PatientComplaintRepository:BaseRepository<PatientComplaint>
    {
        private EPrescriptionDbContext _context;
        public PatientComplaintRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
