using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class GenericNameMedicineRepository:BaseRepository<GenericNameMedicineRelation>
    {
        private EPrescriptionDbContext _context;
        public GenericNameMedicineRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
