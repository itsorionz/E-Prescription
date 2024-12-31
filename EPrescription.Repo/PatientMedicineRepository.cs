using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public  class PatientMedicineRepository:BaseRepository<PatientMedicine>
    {
        private EPrescriptionDbContext _context;

        public PatientMedicineRepository(EPrescriptionDbContext context):base(context)
        {
            _context = context;
        }
    }
}
