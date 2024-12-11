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
    public class DosageTypeModel: DosageType
    {
        private DosageTypeService dosageTypeService;

        [Required]
        [Remote("IsDosageTypeExist", "DosageType", AdditionalFields = "InitialTypeName", ErrorMessage = "Dosage Type already Exist")]
        [Display(Name = "Dosage Type")]
        public new string TypeName
        {
            get { return base.TypeName; }
            set { base.TypeName = value; }
        }

        public DosageTypeModel()
        {
            dosageTypeService = new DosageTypeService();
        }

        public DosageTypeModel(int id):this()
        {
            var dosageTypeEntry = dosageTypeService.GetDosageTypeById(id);
            if (dosageTypeEntry != null)
            {
                base.Id = dosageTypeEntry.Id;
                base.TypeName = dosageTypeEntry.TypeName;
                base.StatusFlag = dosageTypeEntry.StatusFlag;
                base.UpdatedAt = dosageTypeEntry.UpdatedAt;
                base.UpdatedBy = dosageTypeEntry.UpdatedBy;
                base.CreatedAt = dosageTypeEntry.CreatedAt;
                base.CreatedBy = dosageTypeEntry.CreatedBy;
            }
        }

        public IEnumerable<DosageType> GetDosageTypes()
        {
            return dosageTypeService.GetAllDosageTypes().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        }

        public void Add()
        {
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            dosageTypeService.Add(this);
        }
        public void Edit()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            dosageTypeService.Edit(this);
        }
        public bool IsDosageTypeExist(string typeName, string initialTypeName)
        {
            return dosageTypeService.IsDosageTypeExist( typeName, initialTypeName);
        }
        public void Inactive()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Inactive;
            dosageTypeService.Inactive(this);
        }
    }
}