using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public  class PatientProcedureRepository:BaseRepository<PatientProcedure>
    {
        private EPrescriptionDbContext _context;

        public PatientProcedureRepository(EPrescriptionDbContext context):base(context)
        {
            _context = context;
        }
    }
}
