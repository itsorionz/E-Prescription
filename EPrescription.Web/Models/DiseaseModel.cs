using EPrescription.Common;
using EPrescription.Entities;
using EPrescription.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPrescription.Web.Models
{
    public class DiseaseModel: Disease
    {
        private DiseaseService diseaseService;
        [Required]
        [Remote("IsDiseaseNameExist", "Disease", AdditionalFields = "InitialDiseaseName",
            ErrorMessage = "Disease already Exist")]
        [Display(Name = "Disease Name")]
        public new string DiseaseName
        {
            get { return base.DiseaseName; }
            set { base.DiseaseName = value; }
        }


        public DiseaseModel()
        {
            diseaseService = new DiseaseService();
        }

        public DiseaseModel(int id):this()
        {
            var diseaseEntry = diseaseService.GetDiseaseById(id);
            if (diseaseEntry != null)
            {
                base.Id = diseaseEntry.Id;
                base.CreatedAt = diseaseEntry.CreatedAt;
                base.DiseaseName = diseaseEntry.DiseaseName;
                base.CreatedAt = diseaseEntry.CreatedAt;
                base.UpdatedAt = diseaseEntry.UpdatedAt;
                base.UpdatedBy = diseaseEntry.UpdatedBy;
                base.StatusFlag = diseaseEntry.StatusFlag;
            }
        }

        public void Edit()
        {
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            diseaseService.Edit(this);
        }
        public IEnumerable<Disease> GetAllDiseases()
        {
            return diseaseService.GetAllDiseases().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        }

        public void Add()
        {
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            diseaseService.Add(this);
        }
        public bool IsDiseaseNameExist(string diseaseName, string initialDiseaseName)
        {
            return diseaseService.IsDiseaseNameExist( diseaseName,  initialDiseaseName);
        }
        public void Inactive()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Inactive;
            diseaseService.Inactive(this);
        }
    }
}