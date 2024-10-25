using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class PatientInvestigationRepository:Repository<PatientInvestigation>
    {
        private EPrescriptionDbContext _context;

        public PatientInvestigationRepository(EPrescriptionDbContext context):base(context)
        {
            _context = context;
        }
    }
}
