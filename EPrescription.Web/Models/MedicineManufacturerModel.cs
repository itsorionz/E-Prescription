using EPrescription.Common;
using EPrescription.Entities;
using EPrescription.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPrescription.Web.Models
{
    public class MedicineManufacturerModel: MedicineManufacturer
    {
        private MedicineManufacturerService medicineManufacturerService;
        public MedicineManufacturerModel()
        {
            medicineManufacturerService = new MedicineManufacturerService();
        }

        public IEnumerable<MedicineManufacturer> GetAllMedicineManufacturers()
        {
            return medicineManufacturerService.GetAllMedicineManufacturers();
        }

        public void Add()
        {
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            medicineManufacturerService.Add(this);
        }
    }
}