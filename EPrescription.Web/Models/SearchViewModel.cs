using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EPrescription.Entities;
using EPrescription.Services;
using EPrescription.Common;
using PagedList;

namespace EPrescription.Web.Models
{

    public class UserSearchModel
    {
        [Display(Name = "Name")]
        public string SName { get; set; }

        [Display(Name = "Role")]
        public int? SUserRoleId { get; set; }
        public byte? SStatusFlag { get; set; }
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }

        public String Sort { get; set; }
        public String SortDir { get; set; }
        public Int32 TotalRecords { get; set; }

        public List<Role> RoleList { get; set; }


        public IPagedList<User> UserPagedList;
        public UserSearchModel()
        {

            Page = 1;
            PageSize = 25;
            RoleList = new RoleModel().GetAllRoles();

        }
    }
    public class MedicineViewModel
    {
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public List<MedicineManufacturer> MedicineManufacturerList { get; set; }
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }
        public string GenericName { get; set; }
        public string Strength { get; set; } 
        public string DosageType { get; set; }
        public byte StatusFlag { get; set; }
        public string Dar { get; set; }
        public IPagedList<Medicine> MedicinePagedList;
        public MedicineViewModel()
        {
            Page = 1;
            PageSize = 12;
            MedicineManufacturerList = new MedicineManufacturerModel().GetAllMedicineManufacturers().ToList();
        }
    }
    public class PatientViewModel
    {
        public string Name { get; set; }
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }

        public IPagedList<Patient> PatientIPagedList;
        public PatientViewModel()
        {
            Page = 1;
            PageSize = 25;
        }
    }

}