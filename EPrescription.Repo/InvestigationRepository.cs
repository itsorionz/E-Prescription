using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
  public  class InvestigationRepository:Repository<Investigation>
    {
        private EPrescriptionDbContext _context;
        public InvestigationRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }

        public bool IsInvestigationNameExist(string investigationName, string initialInvestigationName)
        {
            bool isNotExist = true;
            if (investigationName != string.Empty && initialInvestigationName == "undefined")
            {
                var isExist = _context.Investigations.Any(x => x.InvestigationName.ToLower().Equals(investigationName.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (investigationName != string.Empty && initialInvestigationName != "undefined")
            {
                var isExist = _context.Investigations.Any(x => x.InvestigationName.ToLower() == investigationName.ToLower() && x.InvestigationName.ToLower() != initialInvestigationName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }
    }
}
