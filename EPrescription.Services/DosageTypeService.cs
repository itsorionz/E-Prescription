using EPrescription.Common;
using EPrescription.Entities;
using EPrescription.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Services
{
   public class DosageTypeService
    {
        private EPrescriptionDbContext _context;
        private DosageTypeUnitOfWork dosageTypeUnitOfWork;
        public DosageTypeService()
        {
            _context = new EPrescriptionDbContext();
            dosageTypeUnitOfWork = new DosageTypeUnitOfWork(_context);
        }

        public IEnumerable<DosageType> GetAllDosageTypes()
        {
            return dosageTypeUnitOfWork.DosageTypeRepository.GetAll().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        }

        public int Add(DosageType dosageType)
        {
            var newDosageType = new DosageType()
            {
                TypeName = dosageType.TypeName,
                StatusFlag = dosageType.StatusFlag,
                CreatedBy = dosageType.CreatedBy,
                UpdatedBy = dosageType.UpdatedBy
            };
            dosageTypeUnitOfWork.DosageTypeRepository.Add(newDosageType);
            dosageTypeUnitOfWork.Save();
            return newDosageType.Id;
        }

        public DosageType GetDosageTypeById(int id)
        {
            return dosageTypeUnitOfWork.DosageTypeRepository.GetById(id);
        }

        public bool IsDosageTypeExist(string typeName, string initialTypeName)
        {
            return dosageTypeUnitOfWork.DosageTypeRepository.IsDosageTypeExist( typeName, initialTypeName);
        }

        public void Edit(DosageType dosageType)
        {
            var dosageTypeEntry = GetDosageTypeById(dosageType.Id);
            if (dosageTypeEntry != null)
            {
                dosageTypeEntry.TypeName = dosageType.TypeName;
                dosageTypeEntry.UpdatedAt = dosageType.UpdatedAt;
                dosageTypeEntry.UpdatedBy = dosageType.UpdatedBy;
                dosageTypeUnitOfWork.DosageTypeRepository.Update(dosageTypeEntry);
                dosageTypeUnitOfWork.Save();
            }
        }

        public void Inactive(DosageType dosageType)
        {
            var dosageTypeEntry = GetDosageTypeById(dosageType.Id);
            if (dosageTypeEntry != null)
            {
                dosageTypeEntry.UpdatedAt = dosageType.UpdatedAt;
                dosageTypeEntry.UpdatedBy = dosageType.UpdatedBy;
                dosageTypeUnitOfWork.DosageTypeRepository.DeleteByItem(dosageTypeEntry);
                dosageTypeUnitOfWork.Save();
            }
        }

        public DosageType GetDosageByName(string strDosageType)
        {
            return dosageTypeUnitOfWork.DosageTypeRepository.GetDosageTypeByName(strDosageType);

        }
    }
}
