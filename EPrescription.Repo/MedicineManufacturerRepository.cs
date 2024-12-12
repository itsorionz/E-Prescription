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

        public bool IsCompanyNameExist(string companyName, string initialCompanyName)
        {
            bool isNotExist = true;
            if (companyName != string.Empty && initialCompanyName == "undefined")
            {
                var isExist = _context.MedicineManufacturers.Any(x => x.CompanyName.ToLower().Equals(companyName.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (companyName != string.Empty && initialCompanyName != "undefined")
            {
                var isExist = _context.MedicineManufacturers.Any(x => x.CompanyName.ToLower() == companyName.ToLower() && x.CompanyName.ToLower() != initialCompanyName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }
    }
}
