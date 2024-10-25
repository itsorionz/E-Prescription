using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
  public  class PatientDiseaseRepository:BaseRepository<PatientDisease>
    {
        private EPrescriptionDbContext _context;
        public PatientDiseaseRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
