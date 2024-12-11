using EPrescription.Entities;
using EPrescription.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Services
{
   public class StrengthService
    {
        private EPrescriptionDbContext _context;
        private StrengthUnitOfWork strengthUnitOfWork;

        public StrengthService()
        {
            _context = new EPrescriptionDbContext();
            strengthUnitOfWork = new StrengthUnitOfWork(_context);
        }

        public IEnumerable<Strength> GetAllStrength()
        {
            return strengthUnitOfWork.StrengthRepository.GetAll();
        }
        public Strength GetStrengthtById(int id)
        {
            return strengthUnitOfWork.StrengthRepository.GetById(id);
        }

        public bool IsStrengthNameExist(string StrengthName, string initialStrengthName)
        {
            return strengthUnitOfWork.StrengthRepository.IsStrengthNameExist(StrengthName, initialStrengthName);
        }

        public int Add(Strength strength)
        {
            var newStrength = new Strength()
            {
                StatusFlag = strength.StatusFlag,
                StrengthName = strength.StrengthName,
                CreatedAt = strength.CreatedAt,
                CreatedBy = strength.CreatedBy,
                UpdatedAt = strength.UpdatedAt,
            };
            strengthUnitOfWork.StrengthRepository.Add(newStrength);
            strengthUnitOfWork.Save();
            return newStrength.Id;
        }

        public Strength GetStrengthByName(string strStrengtName)
        {
            return strengthUnitOfWork.StrengthRepository.GetStrengthByName(strStrengtName);
        }
        public void Edit(Strength strength)
        {
            var strengthEntry = GetStrengthtById(strength.Id);
            if (strengthEntry != null)
            {
                strengthEntry.StrengthName = strength.StrengthName;
                strengthEntry.UpdatedAt = strength.UpdatedAt;
                strengthEntry.UpdatedBy = strength.UpdatedBy;
                strengthUnitOfWork.StrengthRepository.Update(strengthEntry);
                strengthUnitOfWork.Save();
            }
        }
        public void Inactive(Strength strength)
        {
            var strengthEntry = GetStrengthtById(strength.Id);
            if (strengthEntry != null)
            {
                strengthEntry.UpdatedAt = strength.UpdatedAt;
                strengthEntry.UpdatedBy = strength.UpdatedBy;
                strengthUnitOfWork.StrengthRepository.DeleteByItem(strengthEntry);
                strengthUnitOfWork.Save();
            }
        }
    }
}
