using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class DiseaseRepository:Repository<Disease>
    {
        private EPrescriptionDbContext _context;
        public DiseaseRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }

        public bool IsDiseaseNameExist(string diseaseName, string initialDiseaseName)
        {
            bool isNotExist = true;
            if (diseaseName != string.Empty && initialDiseaseName == "undefined")
            {
                var isExist = _context.Diseases.Any(x => x.DiseaseName.ToLower().Equals(diseaseName.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (diseaseName != string.Empty && initialDiseaseName != "undefined")
            {
                var isExist = _context.Diseases.Any(x => x.DiseaseName.ToLower() == diseaseName.ToLower() && x.DiseaseName.ToLower() != initialDiseaseName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }
    }
}
