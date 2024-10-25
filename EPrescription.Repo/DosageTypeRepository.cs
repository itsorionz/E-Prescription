using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class DosageTypeRepository:Repository<DosageType>
    {
        private EPrescriptionDbContext _context;
        public DosageTypeRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }

        public bool IsDosageTypeExist(string typeName, string initialTypeName)
        {
            bool isNotExist = true;
            if (typeName != string.Empty && initialTypeName == "undefined")
            {
                var isExist = _context.DosageTypes.Any(x => x.TypeName.ToLower().Equals(typeName.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (typeName != string.Empty && initialTypeName != "undefined")
            {
                var isExist = _context.DosageTypes.Any(x => x.TypeName.ToLower() == typeName.ToLower() && x.TypeName.ToLower() != typeName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }

        public DosageType GetDosageTypeByName(string strDosageType)
        {
            return _context.DosageTypes.FirstOrDefault(s => s.TypeName.ToUpper() == strDosageType.ToUpper());
        }
    }
}
