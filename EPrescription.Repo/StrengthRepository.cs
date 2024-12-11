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

        public bool IsStrengthNameExist(string strengthName, string initialStrengthName)
        {
            bool isNotExist = true;
            if (strengthName != string.Empty && initialStrengthName == "undefined")
            {
                var isExist = _context.Strengths.Any(x => x.StrengthName.ToLower().Equals(strengthName.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (strengthName != string.Empty && initialStrengthName != "undefined")
            {
                var isExist = _context.Strengths.Any(x => x.StrengthName.ToLower() == strengthName.ToLower() && x.StrengthName.ToLower() != initialStrengthName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }

        public Strength GetStrengthByName(string strStrengtName)
        {
            return _context.Strengths.FirstOrDefault(s => s.StrengthName.ToUpper() == strStrengtName.ToUpper());
        }
    }
}
