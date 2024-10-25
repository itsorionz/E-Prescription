using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
    public class MedicineUnitOfWork : IDisposable
    {
        private EPrescriptionDbContext _context;
        private MedicineRepository medicineRepository;
        private StrengthMedicineRepository strengthMedicineRepository;
        private DosageTypeMedicineRepository dosageTypeMedicineRepository;
        private GenericNameMedicineRepository genericNameMedicineRepository;
        public MedicineUnitOfWork(EPrescriptionDbContext context)
        {
            _context = context;
            medicineRepository = new MedicineRepository(_context);
            strengthMedicineRepository = new StrengthMedicineRepository(_context);
            dosageTypeMedicineRepository = new DosageTypeMedicineRepository(_context);
            genericNameMedicineRepository = new GenericNameMedicineRepository(_context);
        }
        public MedicineRepository MedicineRepository
        {
            get
            {
                return medicineRepository;
            }
        }
        public StrengthMedicineRepository StrengthMedicineRepository
        {
            get
            {
                return strengthMedicineRepository;
            }
        }
        public DosageTypeMedicineRepository DosageTypeMedicineRepository
        {
            get
            {
                return dosageTypeMedicineRepository;
            }
        }
        public GenericNameMedicineRepository GenericNameMedicineRepository
        {
            get
            {
                return genericNameMedicineRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
