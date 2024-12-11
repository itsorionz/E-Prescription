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
    public class StrengthModel: Strength
    {
        private StrengthService strengthService;

        [Required]
        [Remote("IsStrengthNameExist", "Strength", AdditionalFields = "InitialStrengthName", ErrorMessage = "StrengthName already Exist")]
        [Display(Name = "Strength Name")]
        public new string StrengthName
        {
            get { return base.StrengthName; }
            set { base.StrengthName = value; }
        }

        public StrengthModel()
        {
            strengthService = new StrengthService();
        }

        public IEnumerable<Strength> GetAllStrengths()
        {
            return strengthService.GetAllStrength().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        }

        public void Add()
        {
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            strengthService.Add(this);
        }
        public StrengthModel(int id) : this()
        {
            var strengthEntry = strengthService.GetStrengthtById(id);
            if (strengthEntry != null)
            {
                base.Id = strengthEntry.Id;
                base.StrengthName = strengthEntry.StrengthName;
                base.CreatedAt = strengthEntry.CreatedAt;
                base.StatusFlag = strengthEntry.StatusFlag;
                base.UpdatedAt = strengthEntry.UpdatedAt;
                base.UpdatedBy = strengthEntry.UpdatedBy;
                base.CreatedBy = strengthEntry.CreatedBy;
            }
        }

        public void Edit()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            strengthService.Edit(this);
        }
        public bool IsComplaintTypeExist(string StrengthName, string initialStrengthName)
        {
            return strengthService.IsStrengthNameExist(StrengthName, initialStrengthName);
        }
        public void Inactive()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Inactive;
            strengthService.Inactive(this);
        }

    }
}