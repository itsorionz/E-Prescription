using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class DosageTypeMedicineRepository:BaseRepository<DosageTypeMedicineRelation>
    {
        private EPrescriptionDbContext _context;
        public DosageTypeMedicineRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
