using EPrescription.Common;
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

        public IEnumerable<string> GetAvailablity(string medicineName)
        {
            return medicineUnitOfWork.MedicineRepository.GetAvailablity(medicineName);
        }

        public IEnumerable<string> GetMedicinesNameByStr(string name)
        {
            return medicineUnitOfWork.MedicineRepository.GetMedicineNameByStr(name);
        }

        public IPagedList<Medicine> GetAllIPagedMedicine(int page, int pageSize, string name, int? companyId, string genericName)
        {
            return medicineUnitOfWork.MedicineRepository.GetAllIPagedMedicine(page, pageSize, name, companyId, genericName);
        }

        public Medicine GetMedicineById(int id)
        {
            return medicineUnitOfWork.MedicineRepository.GetById(id);
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

        public void Edit(Medicine model)
        {
            var medicineEntry = medicineUnitOfWork.MedicineRepository.GetById(model.Id);
            if (medicineEntry != null)
            {
                medicineEntry.BrandName = model.BrandName;
                medicineEntry.MedicineManufacturerId = model.MedicineManufacturerId;
                medicineEntry.StatusFlag = model.StatusFlag;
                medicineEntry.UseFor = model.UseFor;
                medicineEntry.Dar = model.Dar;
                medicineUnitOfWork.MedicineRepository.Update(medicineEntry);
                medicineUnitOfWork.Save();
            }
         }

        public List<int> GetGenericNameIdsByMedicineId(int medicineId)
        {
            var genericNameIds = medicineUnitOfWork.GenericNameMedicineRepository.GetAll().Where(rel => rel.MedicineId == medicineId).Select(rel => rel.Id).ToList();
            return genericNameIds;
        }

        public List<int> GetStrengthIdsByMedicineId(int medicineId)
        {
            var strengthIds = medicineUnitOfWork.StrengthMedicineRepository.GetAll().Where(rel => rel.MedicineId == medicineId).Select(rel => rel.Id).ToList();
            return strengthIds;
        }
        
        public List<int> GetDosageTypeIdsByMedicineId(int medicineId)
        {
            var dosageTypeRelations = medicineUnitOfWork.DosageTypeMedicineRepository.GetAll().Where(rel => rel.MedicineId == medicineId).Select(rel => rel.Id).ToList();
            return dosageTypeRelations;
        }
        public void UpdateGenericNameRelation(GenericNameMedicineRelation genericNameMedicineRelation)
        {
            var newGenericNameRelation = new GenericNameMedicineRelation()
            {
                GenericTypeId = genericNameMedicineRelation.GenericTypeId,
                MedicineId = genericNameMedicineRelation.MedicineId,
                StatusFlag = genericNameMedicineRelation.StatusFlag
            };
            medicineUnitOfWork.GenericNameMedicineRepository.Update(newGenericNameRelation);
            medicineUnitOfWork.Save();
        }
        public void UpdateStrengthRelation(StrengthMedicineRelation strengthMedicineRelation)
        {
            var newStrengthRelation = new StrengthMedicineRelation()
            {
                MedicineId = strengthMedicineRelation.MedicineId,
                StrengthId = strengthMedicineRelation.StrengthId,
                StatusFlag = strengthMedicineRelation.StatusFlag
            };
            medicineUnitOfWork.StrengthMedicineRepository.Update(newStrengthRelation);
            medicineUnitOfWork.Save();
        }
        public void UpdateDosageTypeRelation(DosageTypeMedicineRelation dosageTypeMedicineRelation)
        {
            var newDosageTypeRelation = new DosageTypeMedicineRelation()
            {
                DosageTypeId = dosageTypeMedicineRelation.DosageTypeId,
                MedicineId = dosageTypeMedicineRelation.MedicineId,
                StatusFlag = dosageTypeMedicineRelation.StatusFlag
            };
            medicineUnitOfWork.DosageTypeMedicineRepository.Update(newDosageTypeRelation);
            medicineUnitOfWork.Save();
        }

        public void Inactive(Medicine medicine)
        {
            var medicineEntry = GetMedicineById(medicine.Id);
            if (medicineEntry != null)
            {
                medicineEntry.UpdatedAt = medicine.UpdatedAt;
                medicineEntry.UpdatedBy = medicine.UpdatedBy;
                medicineUnitOfWork.MedicineRepository.DeleteByItem(medicineEntry);
                medicineUnitOfWork.Save();
            }
        }


    }
}
