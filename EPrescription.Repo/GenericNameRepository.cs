using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
  public class GenericNameRepository:Repository<GenericName>
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
    }
}
