using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class StrengthRepository:Repository<Strength>
    {
        private EPrescriptionDbContext _context;
        public StrengthRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }

        public Strength GetStrengthByName(string strStrengtName)
        {
            return _context.Strengths.FirstOrDefault(s => s.StrengthName.ToUpper() == strStrengtName.ToUpper());
        }
    }
}
