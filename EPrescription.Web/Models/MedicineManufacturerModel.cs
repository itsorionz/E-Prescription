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
    public class MedicineManufacturerModel: MedicineManufacturer
    {
        private MedicineManufacturerService medicineManufacturerService;

        [Required]
        [Remote("IsCompanyNameExist", "MedicineManufacturer", AdditionalFields = "InitialCompanyName", ErrorMessage = "Company Name already Exist")]
        [Display(Name = "Company Name")]
        public new string CompanyName
        {
            get { return base.CompanyName; }
            set { base.CompanyName = value; }
        }

        public MedicineManufacturerModel()
        {
            medicineManufacturerService = new MedicineManufacturerService();
        }

        public MedicineManufacturerModel(int id) : this()
        {
            var manufacturerEntry = medicineManufacturerService.GetManufacturerById(id);
            if (manufacturerEntry != null)
            {
                base.Id = manufacturerEntry.Id;
                base.CompanyName = manufacturerEntry.CompanyName;
                base.ContactNumber = manufacturerEntry.ContactNumber;
                base.Email = manufacturerEntry.Email;
                base.Address = manufacturerEntry.Address;
                base.CreatedAt = manufacturerEntry.CreatedAt;
                base.StatusFlag = manufacturerEntry.StatusFlag;
                base.UpdatedAt = manufacturerEntry.UpdatedAt;
                base.UpdatedBy = manufacturerEntry.UpdatedBy;
                base.CreatedBy = manufacturerEntry.CreatedBy;
            }
        }

        public IEnumerable<MedicineManufacturer> GetAllMedicineManufacturers()
        {
            return medicineManufacturerService.GetAllMedicineManufacturers().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        }

        public void Add()
        {
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            medicineManufacturerService.Add(this);
        }

        public void Edit()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            medicineManufacturerService.Edit(this);
        }

        public bool IsCompanyNameExist(string companyName, string InitialCompanyName)
        {
            return medicineManufacturerService.IsCompanyNameExist(companyName, InitialCompanyName);
        }
        public void Inactive()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Inactive;
            medicineManufacturerService.Inactive(this);
        }

    }
}