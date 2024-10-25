using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class DoctorRepository:Repository<Doctor>
    {
        private EPrescriptionDbContext _context;

        public DoctorRepository(EPrescriptionDbContext context):base(context)
        {
            _context = context;
        }

        public Doctor GetFirstDefault()
        {
            return _context.Doctors.FirstOrDefault();
        }
    }
}
