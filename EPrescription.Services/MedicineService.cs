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
            return medicineUnitOfWork.MedicineRepository.GetAll().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        }

        //public IEnumerable<GenericNameMedicineRelation> GetAllGenericNameMedicineRelation()
        //{
        //    return medicineUnitOfWork.GenericNameMedicineRepository.GetAll().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        //}
        //public IEnumerable<StrengthMedicineRelation> GetAllStrengthMedicineRelation()
        //{
        //    return medicineUnitOfWork.StrengthMedicineRepository.GetAll().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        //}
        //public IEnumerable<DosageTypeMedicineRelation> GetAllDosageTypeMedicineRelation()
        //{
        //    return medicineUnitOfWork.DosageTypeMedicineRepository.GetAll().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        //}
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
            var genericNameIds = medicineUnitOfWork.GenericNameMedicineRepository
                .GetAll()
                .Where(rel => rel.MedicineId == medicineId && rel.GenericTypeId.HasValue && rel.StatusFlag == (byte)EnumActiveDeative.Active)
                .Select(rel => rel.GenericTypeId.Value)
                .ToList();
            return genericNameIds;
        }

        public List<int> GetStrengthIdsByMedicineId(int medicineId)
        {
            var strengthIds = medicineUnitOfWork.StrengthMedicineRepository
                .GetAll()
                .Where(rel => rel.MedicineId == medicineId && rel.StrengthId.HasValue && rel.StatusFlag == (byte)EnumActiveDeative.Active)
                .Select(rel => rel.StrengthId.Value)
                .ToList();
            return strengthIds;
        }
        
        public List<int> GetDosageTypeIdsByMedicineId(int medicineId)
        {
            var dosageTypeRelations = medicineUnitOfWork.DosageTypeMedicineRepository
                .GetAll()
                .Where(rel => rel.MedicineId == medicineId && rel.DosageTypeId.HasValue && rel.StatusFlag == (byte)EnumActiveDeative.Active)
                .Select(rel => rel.DosageTypeId.Value)
                .ToList();
            return dosageTypeRelations;
        }
        public void UpdateGenericNameRelations(int medicineId, List<int> genericNameIds)
        {
            var existingRelations = medicineUnitOfWork.GenericNameMedicineRepository
                .GetAll()
                .Where(r => r.MedicineId == medicineId && r.StatusFlag == (byte)EnumActiveDeative.Active)
                .ToList();

            var existingGenericTypeIds = existingRelations
                .Where(r => r.GenericTypeId.HasValue)
                .Select(r => r.GenericTypeId.Value)
                .ToList();

            var relationsToRemove = existingRelations
                .Where(r => !genericNameIds.Contains(r.GenericTypeId ?? 0))
                .ToList();

            var idsToAdd = genericNameIds
                .Where(id => !existingGenericTypeIds.Contains(id))
                .ToList();

            foreach (var relation in relationsToRemove)
            {
                medicineUnitOfWork.GenericNameMedicineRepository.DeleteByItem(relation);
            }

            foreach (var id in idsToAdd)
            {
                var newRelation = new GenericNameMedicineRelation
                {
                    MedicineId = medicineId,
                    GenericTypeId = id,
                    StatusFlag = (byte)EnumActiveDeative.Active
                };
                medicineUnitOfWork.GenericNameMedicineRepository.Add(newRelation);
            }
            medicineUnitOfWork.Save();
        }

        public void UpdateStrengthRelations(int medicineId, List<int> strengthIds)
        {
            var existingRelations = medicineUnitOfWork.StrengthMedicineRepository
                .GetAll()
                .Where(r => r.MedicineId == medicineId && r.StatusFlag == (byte)EnumActiveDeative.Active)
                .ToList();

            var existingStrengthIds = existingRelations
                .Select(r => r.StrengthId)
                .ToList();

            var relationsToRemove = existingRelations
                .Where(r => !strengthIds.Contains(r.StrengthId ?? 0))
                .ToList();

            var idsToAdd = strengthIds
                .Where(id => !existingStrengthIds.Contains(id))
                .ToList();

            foreach (var relation in relationsToRemove)
            {
                medicineUnitOfWork.StrengthMedicineRepository.DeleteByItem(relation);
            }

            foreach (var id in idsToAdd)
            {
                var newRelation = new StrengthMedicineRelation
                {
                    MedicineId = medicineId,
                    StrengthId = id,
                    StatusFlag = (byte)EnumActiveDeative.Active
                };
                medicineUnitOfWork.StrengthMedicineRepository.Add(newRelation);
            }
            medicineUnitOfWork.Save();
        }


        public void UpdateDosageTypeRelations(int medicineId, List<int> dosageTypeIds)
        {
            var existingRelations = medicineUnitOfWork.DosageTypeMedicineRepository
                .GetAll()
                .Where(r => r.MedicineId == medicineId && r.StatusFlag == (byte)EnumActiveDeative.Active)
                .ToList();

            var existingDosageTypeIds = existingRelations
                .Select(r => r.DosageTypeId)
                .ToList();

            var relationsToRemove = existingRelations
                .Where(r => !dosageTypeIds.Contains(r.DosageTypeId ?? 0))
                .ToList();

            var idsToAdd = dosageTypeIds
                .Where(id => !existingDosageTypeIds.Contains(id))
                .ToList();

            foreach (var relation in relationsToRemove)
            {
                medicineUnitOfWork.DosageTypeMedicineRepository.DeleteByItem(relation);
            }

            foreach (var id in idsToAdd)
            {
                var newRelation = new DosageTypeMedicineRelation
                {
                    MedicineId = medicineId,
                    DosageTypeId = id,
                    StatusFlag = (byte)EnumActiveDeative.Active
                };
                medicineUnitOfWork.DosageTypeMedicineRepository.Add(newRelation);
            }
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
