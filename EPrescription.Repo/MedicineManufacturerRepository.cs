using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class MedicineManufacturerRepository:Repository<MedicineManufacturer>
    {
        private EPrescriptionDbContext _context;
        public MedicineManufacturerRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }

        public MedicineManufacturer GetManufacturerByName(string strCompanyName)
        {
            return _context.MedicineManufacturers.FirstOrDefault(s => s.CompanyName.ToUpper() == strCompanyName.ToUpper());
        }
    }
}
