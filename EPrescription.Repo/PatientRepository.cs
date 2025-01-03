using EPrescription.Common;
using EPrescription.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public  class PatientRepository : BaseRepository<Patient>
    {
        private EPrescriptionDbContext _context;
        public PatientRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }

        public IPagedList<Patient> GetAllIPagedList(int page, int pageSize, string name)
        {
            return _context.Patients
                .Where(s => (name == null || s.Name.ToUpper().Contains(name.ToUpper())) && (s.StatusFlag == (byte)EnumActiveDeative.Active))
                .OrderByDescending(s => s.Id)
                .ToPagedList(page, pageSize);
        }
        public int GetCount(DateTime dt)
        {
            return _context.Patients.Count(s => s.CreatedAt.Value.Year == dt.Year && s.CreatedAt.Value.Month == dt.Month && s.CreatedAt.Value.Day == dt.Day);
        }
    }
}
