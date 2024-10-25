using EPrescription.Entities;
using EPrescription.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Services
{
  public  class MedicineManufacturerService
    {
        private EPrescriptionDbContext _context;
        private MedicineManufacturerUnitOfWork medicineManufacturerUnitOfWork;
        public MedicineManufacturerService()
        {
            _context = new EPrescriptionDbContext();
            medicineManufacturerUnitOfWork = new MedicineManufacturerUnitOfWork(_context);

        }

        public IEnumerable<MedicineManufacturer> GetAllMedicineManufacturers()
        {
            return medicineManufacturerUnitOfWork.MedicineManufacturerRepository.GetAll();
        }

        public int Add(MedicineManufacturer medicineManufacturer)
        {
            var newMedicineManufacturer = new MedicineManufacturer()
            {
                ContactNumber = medicineManufacturer.ContactNumber,
                CompanyName = medicineManufacturer.CompanyName,
                Address = medicineManufacturer.Address,
                Email = medicineManufacturer.Email,
                StatusFlag = medicineManufacturer.StatusFlag,
                CreatedAt = medicineManufacturer.CreatedAt,
                CreatedBy = medicineManufacturer.CreatedBy,
                UpdatedAt = medicineManufacturer.UpdatedAt,
                UpdatedBy = medicineManufacturer.UpdatedBy
            };
            medicineManufacturerUnitOfWork.MedicineManufacturerRepository.Add(newMedicineManufacturer);
            medicineManufacturerUnitOfWork.Save();
            return newMedicineManufacturer.Id;
        }

        public MedicineManufacturer GetManufacturerByName(string strCompanyName)
        {
            return medicineManufacturerUnitOfWork.MedicineManufacturerRepository.GetManufacturerByName(strCompanyName);
        }
    }
}
