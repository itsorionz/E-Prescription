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
    public class GenericNameModel:GenericName
    {
        private GenericNameService genericNameService;

        [Required]
        [Remote("IsGenericTypeExist", "GenericName", AdditionalFields = "InitialGenericType", ErrorMessage = "Generic Type already Exist")]
        [Display(Name = "Generic Type")]
        public new string TypeName
        {
            get { return base.TypeName; }
            set { base.TypeName = value; }
        }
        public GenericNameModel()
        {
            genericNameService = new GenericNameService();
        }

        public GenericNameModel(int id) : this()
        {
            var genericEntry = genericNameService.GetGenericById(id);
            if (genericEntry != null)
            {
                base.Id = genericEntry.Id;
                base.TypeName = genericEntry.TypeName;
                base.CreatedAt = genericEntry.CreatedAt;
                base.StatusFlag = genericEntry.StatusFlag;
                base.UpdatedAt = genericEntry.UpdatedAt;
                base.UpdatedBy = genericEntry.UpdatedBy;
                base.CreatedBy = genericEntry.CreatedBy;
            }
        }

        public List<GenericName> GetAllGenericNames()
        {
            return genericNameService.GetAllGenericNames().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active).ToList();
        }

        public void Add()
        {
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            genericNameService.Add(this);
        }

        public bool IsGenericTypeExist(string genericType, string initialGenericType)
        {
            return genericNameService.IsGenericTypeExist(genericType, initialGenericType);
        }
        public void Edit()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            genericNameService.Edit(this);
        }
        public void Inactive()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Inactive;
            genericNameService.Inactive(this);
        }
    }
}