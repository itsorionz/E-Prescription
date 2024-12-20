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
   public class GenericNameService
    {
        private EPrescriptionDbContext _context;
        private GenericNameUnitOfWork genericNameUnitOfWork;

        public GenericNameService()
        {
            _context = new EPrescriptionDbContext();
            genericNameUnitOfWork = new GenericNameUnitOfWork(_context);

        }

        public IEnumerable<GenericName> GetAllGenericNames()
        {
            return genericNameUnitOfWork.GenericNameRepository.GetAll().Where(s=> s.StatusFlag == (byte)EnumActiveDeative.Active);
        }

        public int Add(GenericName genericName)
        {
            var newGenericName = new GenericName()
            {
                TypeName = genericName.TypeName,
                CreatedAt = genericName.CreatedAt,
                CreatedBy = genericName.CreatedBy,
                UpdatedBy = genericName.UpdatedBy,
                StatusFlag = genericName.StatusFlag,
                UpdatedAt = genericName.UpdatedAt
            };
            genericNameUnitOfWork.GenericNameRepository.Add(newGenericName);
            genericNameUnitOfWork.Save();
            return newGenericName.Id;
        }
        public GenericName GetGenericNameByName(string strGenericName)
        {
            return genericNameUnitOfWork.GenericNameRepository.GetGenericNameByName(strGenericName);
        }
        public GenericName GetGenericById(int id)
        {
            return genericNameUnitOfWork.GenericNameRepository.GetById(id);
        }

        public bool IsGenericTypeExist(string genericType, string initialGenericType)
        {
            return genericNameUnitOfWork.GenericNameRepository.IsGenericTypeExist(genericType, initialGenericType);
        }

        public void Edit(GenericName generic)
        {
            var genericEntry = GetGenericById(generic.Id);
            if (genericEntry != null)
            {
                genericEntry.TypeName = generic.TypeName;
                genericEntry.UpdatedAt = generic.UpdatedAt;
                genericEntry.UpdatedBy = generic.UpdatedBy;
                genericNameUnitOfWork.GenericNameRepository.Update(genericEntry);
                genericNameUnitOfWork.Save();
            }
        }
        public void Inactive(GenericName generic)
        {
            var genericEntry = GetGenericById(generic.Id);
            if (genericEntry != null)
            {
                genericEntry.UpdatedAt = generic.UpdatedAt;
                genericEntry.UpdatedBy = generic.UpdatedBy;
                genericNameUnitOfWork.GenericNameRepository.DeleteByItem(genericEntry);
                genericNameUnitOfWork.Save();
            }
        }
    }
}
