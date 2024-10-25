using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class DosageCommentRepository:Repository<DosageComment>
    {
        private EPrescriptionDbContext _context;

        public DosageCommentRepository(EPrescriptionDbContext context):base(context)
        {
            _context = context;
        }
    }
}
