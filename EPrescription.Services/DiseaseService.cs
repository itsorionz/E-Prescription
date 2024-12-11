using EPrescription.Entities;
using EPrescription.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Services
{
   public class DiseaseService
    {
        private EPrescriptionDbContext _context;
        private DiseaseUnitOfWork diseaseUnitOfWork;
        public DiseaseService()
        {
            _context = new EPrescriptionDbContext();
            diseaseUnitOfWork = new DiseaseUnitOfWork(_context);
        }

        public IEnumerable<Disease> GetAllDiseases()
        {
            return diseaseUnitOfWork.DiseaseRepository.GetAll();
        }

        public void Add(Disease disease)
        {
            var newDisease = new Disease()
            {
                DiseaseName = disease.DiseaseName,
                CreatedAt = disease.CreatedAt,
                UpdatedAt = disease.UpdatedAt,
                CreatedBy = disease.CreatedBy,
                UpdatedBy = disease.UpdatedBy,
                StatusFlag = disease.StatusFlag
            };
            diseaseUnitOfWork.DiseaseRepository.Add(newDisease);
            diseaseUnitOfWork.Save();
        }
         public void Edit(Disease disease)
        {
            var diseaseEntry = GetDiseaseById(disease.Id);
            if (diseaseEntry != null)
            {
                diseaseEntry.UpdatedBy = disease.UpdatedBy;
                diseaseEntry.DiseaseName = disease.DiseaseName;
                diseaseUnitOfWork.DiseaseRepository.Update(diseaseEntry);
                diseaseUnitOfWork.Save();
            }
        }

        public Disease GetDiseaseById(int id)
        {
            return diseaseUnitOfWork.DiseaseRepository.GetById(id);
        }

        public bool IsDiseaseNameExist(string diseaseName, string initialDiseaseName)
        {
            return diseaseUnitOfWork.DiseaseRepository.IsDiseaseNameExist(diseaseName,  initialDiseaseName);
        }
        public void Inactive(Disease disease)
        {
            var diseaseEntry = GetDiseaseById(disease.Id);
            if (diseaseEntry != null)
            {
                diseaseEntry.UpdatedBy = disease.UpdatedBy;
                diseaseEntry.DiseaseName = disease.DiseaseName;
                diseaseUnitOfWork.DiseaseRepository.DeleteByItem(diseaseEntry);
                diseaseUnitOfWork.Save();
            }
        }
    }
}
