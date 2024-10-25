using EPrescription.Entities;
using EPrescription.Repo;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Services
{
    public class MedicineService
    {
        private EPrescriptionDbContext _context;
        private MedicineUnitOfWork medicineUnitOfWork;
        public MedicineService()
        {
            _context = new EPrescriptionDbContext();
            medicineUnitOfWork = new MedicineUnitOfWork(_context);
        }

        public IEnumerable<Medicine> GetAllMedicines()
        {
            return medicineUnitOfWork.MedicineRepository.GetAll();
        }

        public int Add(Medicine medicine)
        {
            var newMedicine = new Medicine()
            {
                MedicineManufacturerId = medicine.MedicineManufacturerId,
                BrandName = medicine.BrandName,
                StatusFlag = medicine.StatusFlag,
                CreatedBy = medicine.CreatedBy,
                UseFor = medicine.UseFor,
                Dar = medicine.Dar,
            };
            medicineUnitOfWork.MedicineRepository.Add(newMedicine);
            medicineUnitOfWork.Save();
            return newMedicine.Id;
        }

        public IEnumerable<string> GetAllMedicinesName()
        {
            return medicineUnitOfWork.MedicineRepository.GetAllMedicineName();
        }

        public void AddStrengthRelation(StrengthMedicineRelation strengthMedicineRelation)
        {
            var newStrengthRelation = new StrengthMedicineRelation()
            {
                MedicineId = strengthMedicineRelation.MedicineId,
                StrengthId = strengthMedicineRelation.StrengthId,
                StatusFlag = strengthMedicineRelation.StatusFlag
            };
            medicineUnitOfWork.StrengthMedicineRepository.Add(newStrengthRelation);
            medicineUnitOfWork.Save();
        }

        public IEnumerable<string> GetAvailablity(string medicineName)
        {
            return medicineUnitOfWork.MedicineRepository.GetAvailablity(medicineName);
        }

        public IEnumerable<string> GetMedicinesNameByStr(string name)
        {
            return medicineUnitOfWork.MedicineRepository.GetMedicineNameByStr(name);
        }

        public void AddDosageTypeRelation(DosageTypeMedicineRelation dosageTypeMedicineRelation)
        {
            var newDosageTypeRelation = new DosageTypeMedicineRelation()
            {
                DosageTypeId = dosageTypeMedicineRelation.DosageTypeId,
                MedicineId = dosageTypeMedicineRelation.MedicineId,
                StatusFlag = dosageTypeMedicineRelation.StatusFlag
            };
            medicineUnitOfWork.DosageTypeMedicineRepository.Add(newDosageTypeRelation);
            medicineUnitOfWork.Save();
        }

        public IPagedList<Medicine> GetAllIPagedMedicine(int page, int pageSize, string name, int? companyId, string genericName)
        {
            return medicineUnitOfWork.MedicineRepository.GetAllIPagedMedicine(page, pageSize, name, companyId, genericName);
        }

        public void AddGenericNameRelation(GenericNameMedicineRelation genericNameMedicineRelation)
        {
            var newGenericNameRelation = new GenericNameMedicineRelation()
            {
                GenericTypeId = genericNameMedicineRelation.GenericTypeId,
                MedicineId = genericNameMedicineRelation.MedicineId,
                StatusFlag = genericNameMedicineRelation.StatusFlag
            };
            medicineUnitOfWork.GenericNameMedicineRepository.Add(newGenericNameRelation);
            medicineUnitOfWork.Save();
        }
    }
}
