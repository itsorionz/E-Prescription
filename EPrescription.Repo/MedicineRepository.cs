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
    public class MedicineRepository : Repository<Medicine>
    {
        private EPrescriptionDbContext _context;

        public MedicineRepository(EPrescriptionDbContext context) : base(context)
        {
            _context = context;
        }

        public IPagedList<Medicine> GetAllIPagedMedicine(int page, int pageSize, string name, int? companyId, string genericName)
        {
            return _context.Medicines
                .Where(s =>
                    (name == null || s.BrandName.ToUpper().Contains(name.ToUpper())) &&
                    (companyId == null || s.MedicineManufacturerId == companyId) &&
                    (s.StatusFlag == (byte)EnumActiveDeative.Active) &&
                    (genericName == null ||
                     s.GenericNameMedicineRelations.Any(a =>
                             a.StatusFlag == (byte)EnumActiveDeative.Active &&
                             a.GenericName.TypeName.ToUpper().Contains(genericName.ToUpper()))))
                .OrderBy(o => o.BrandName)
                .ToPagedList(page, pageSize);
        }
        public IEnumerable<string> GetAllMedicineName()
        {
            return _context.Medicines.Select(s => s.BrandName);
        }
        public IEnumerable<string> GetMedicineNameByStr(string name)
        {
            return _context.Medicines.Where(s => s.BrandName.ToLower().StartsWith(name.ToLower())).Select(x => x.BrandName);
        }
        public IEnumerable<string> GetAvailablity(string medicineName)
        {
            return _context.Medicines
                .FirstOrDefault(s => s.BrandName.ToLower()
                .Contains(medicineName.ToLower())).DosageTypeMedicineRelations
                .Select(s => s.DosageType.TypeName)
                .Distinct();
        }
    }
}
