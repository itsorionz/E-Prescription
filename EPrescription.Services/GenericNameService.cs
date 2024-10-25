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
            return genericNameUnitOfWork.GenericNameRepository.GetAll();
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
    }
}
