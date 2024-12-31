using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class GenericNameRepository : Repository<GenericName>
    {
        private EPrescriptionDbContext _context;
        public GenericNameRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }

        public GenericName GetGenericNameByName(string strGenericName)
        {
            return _context.GenericNames.FirstOrDefault(s => s.TypeName.ToUpper() == strGenericName.ToUpper());
        }

        public bool IsGenericTypeExist(string genericType, string initialGenericType)
        {
            bool isNotExist = true;
            if (genericType != string.Empty && initialGenericType == "undefined")
            {
                var isExist = _context.GenericNames
                    .Any(x => x.TypeName.ToLower().Equals(genericType.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (genericType != string.Empty && initialGenericType != "undefined")
            {
                var isExist = _context.GenericNames
                    .Any(x => x.TypeName.ToLower() == genericType.ToLower() 
                    && x.TypeName.ToLower() != initialGenericType.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }
    }
}
