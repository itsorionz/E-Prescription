using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class StrengthMedicineRepository:BaseRepository<StrengthMedicineRelation>
    {
        private EPrescriptionDbContext _context;
        public StrengthMedicineRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
