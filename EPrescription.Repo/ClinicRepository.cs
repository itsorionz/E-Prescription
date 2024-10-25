using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class ClinicRepository:Repository<Clinic>
    {
        private EPrescriptionDbContext _context;

        public ClinicRepository(EPrescriptionDbContext context):base(context)
        {
            _context = context;
        }
    }
}
