using EPrescription.Common;
using EPrescription.Entities;
using EPrescription.Services;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPrescription.Web.Models
{
    public class MedicineModel : Medicine
    {
        private MedicineService medicineService;
        private MedicineManufacturerService medicineManufacturerService;



        private DosageTypeService dosageTypeService;
        private StrengthService strengthService;
        private GenericNameService genericNameService;

        private ComplaintService complaintService;
        private InvestigationService investigationService;
        private DiseaseService diseaseService;
        private ProcedureService procedureService;
        public IEnumerable<MedicineManufacturer> MedicineManufactureList { get; set; }



        public IEnumerable<DosageType> DosageTypeList { get; set; }
        public IEnumerable<GenericName> GenericNameList { get; set; }
        public IEnumerable<Strength> StrengthList { get; set; }
        [Display(Name = "Generic Name")]
        public List<int> GenericNameIds { get; set; }



        [Display(Name = "Dosages")]
        public List<int> DosageIds { get; set; }
        [Display(Name = "Strengths")]
        public List<int> StrengthIds { get; set; }
        public MedicineModel()
        {
            medicineService = new MedicineService();
            medicineManufacturerService = new MedicineManufacturerService();
            dosageTypeService = new DosageTypeService();
            genericNameService = new GenericNameService();
            strengthService = new StrengthService();

            MedicineManufactureList = medicineManufacturerService.GetAllMedicineManufacturers();
            GenericNameList = genericNameService.GetAllGenericNames();
            DosageTypeList = dosageTypeService.GetAllDosageTypes();
            StrengthList = strengthService.GetAllStrength();
        }

        public IEnumerable<string> GetAvailablity(string medicineName)
        {
            return medicineService.GetAvailablity(medicineName);
        }

        public IEnumerable<string> GetMedicineNameByStr(string name)
        {
            return medicineService.GetMedicinesNameByStr(name);
        }

        public IPagedList<Medicine> GetAllIPagedMedicine(int page, int pageSize, string name, int? companyId, string genericName)
        {
            return medicineService.GetAllIPagedMedicine(page, pageSize, name, companyId, genericName);
        }
        public IEnumerable<Medicine> GetAllMedicines()
        {
            return medicineService.GetAllMedicines().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        }
        public void Add()
        {
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            int returnId = medicineService.Add(this);
            foreach (int strengthId in StrengthIds)
            {
                var strengthMedicineRelation = new StrengthMedicineRelation()
                {
                    MedicineId = returnId,
                    StrengthId = strengthId,
                    StatusFlag = (byte)EnumActiveDeative.Active
                };
                medicineService.AddStrengthRelation(strengthMedicineRelation);
            }
            foreach (int dosageTypeId in DosageIds)
            {
                var dosageTypeMedicineRelation = new DosageTypeMedicineRelation()
                {
                    MedicineId = returnId,
                    DosageTypeId = dosageTypeId,
                    StatusFlag = (byte)EnumActiveDeative.Active,
                };
                medicineService.AddDosageTypeRelation(dosageTypeMedicineRelation);
            }
            foreach (int genericId in GenericNameIds)
            {
                var genericNameMedicineRelation = new GenericNameMedicineRelation()
                {
                    GenericTypeId = genericId,
                    MedicineId = returnId,
                    StatusFlag = (byte)EnumActiveDeative.Active
                };
                medicineService.AddGenericNameRelation(genericNameMedicineRelation);
            }
        }
        public string UploadExcel(HttpPostedFileBase excelFile)
        {
            int rowCount = 0;
            int currentUserId = AuthenticatedUser.GetUserFromIdentity().UserId;
            HttpPostedFileBase file = excelFile;
            if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx") ||
                file.FileName.EndsWith("XLS") ||
                file.FileName.EndsWith("XLSX"))
            {
                string fileName = file.FileName;
                string fileContentType = file.ContentType;
                byte[] fileBytes = new byte[file.ContentLength];
                var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                using (var package = new ExcelPackage(file.InputStream))
                {
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet.First();
                    var noOfCol = workSheet.Dimension.End.Column;
                    var noOfRow = workSheet.Dimension.End.Row;
                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                    {

                        int colCompanyName = 2;
                        int colMedicineName = 3;
                        int colGenericName = 4;
                        int colStrength = 5;
                        int colDosageType = 6;
                        int colUseFor = 8;
                        int colDar = 9;
                        if (workSheet.Cells[rowIterator, colMedicineName].Value != null &&
                                   workSheet.Cells[rowIterator, colUseFor].Value.ToString() == "Human")
                        {
                            string strCompanyName = workSheet.Cells[rowIterator, colCompanyName].Value == null ? "" : workSheet.Cells[rowIterator, colCompanyName].Value.ToString();
                            string strMedicineName = workSheet.Cells[rowIterator, colMedicineName].Value == null ? "" : workSheet.Cells[rowIterator, colMedicineName].Value.ToString();
                            string strGenericName = workSheet.Cells[rowIterator, colGenericName].Value == null ? "" : workSheet.Cells[rowIterator, colGenericName].Value.ToString();
                            string strStrength = workSheet.Cells[rowIterator, colStrength].Value == null ? "" : workSheet.Cells[rowIterator, colStrength].Value.ToString();
                            string strDosageType = workSheet.Cells[rowIterator, colDosageType].Value == null ? "" : workSheet.Cells[rowIterator, colDosageType].Value.ToString();
                            string strDar = workSheet.Cells[rowIterator, colDar].Value == null ? "" : workSheet.Cells[rowIterator, colDar].Value.ToString();
                            var manufacturerEntry = medicineManufacturerService.GetManufacturerByName(strCompanyName);
                            int manufacturerId = 0;
                            int medicineId = 0;
                            if (manufacturerEntry != null)
                            {
                                manufacturerId = manufacturerEntry.Id;
                            }
                            else
                            {
                                var newManufacturer = new MedicineManufacturer()
                                {
                                    CompanyName = strCompanyName,
                                    StatusFlag = 1,
                                    CreatedBy = currentUserId
                                };
                                manufacturerId = medicineManufacturerService.Add(newManufacturer);
                            }
                            var newMedicine = new Medicine()
                            {
                                BrandName = strMedicineName,
                                Dar = strDar,
                                StatusFlag = 1,
                                CreatedBy = currentUserId,
                                MedicineManufacturerId = manufacturerId
                            };
                            medicineId = medicineService.Add(newMedicine);
                            //---------Dosage Type------------//
                            string[] dosageTypeChar = strDosageType.Split('+');
                            foreach (string strDT in dosageTypeChar)
                            {
                                int dosageTypeId = 0;
                                var dosageEntry = dosageTypeService.GetDosageByName(strDT);
                                if (dosageEntry != null)
                                {
                                    dosageTypeId = dosageEntry.Id;
                                }
                                else
                                {
                                    var newDosageType = new DosageType()
                                    {
                                        TypeName = strDT,
                                        StatusFlag = 1,
                                        CreatedBy = currentUserId,
                                    };
                                    dosageTypeId = dosageTypeService.Add(newDosageType);
                                }
                                var dosageTypeMedicineRelation = new DosageTypeMedicineRelation()
                                {
                                    MedicineId = medicineId,
                                    DosageTypeId = dosageTypeId,
                                    StatusFlag = 1,
                                };
                                medicineService.AddDosageTypeRelation(dosageTypeMedicineRelation);
                            }
                            //---------Dosage Type------------//

                            //---------Generic Name------------//
                            string[] genericNameChar = strGenericName.Split('+');
                            foreach (string strGN in genericNameChar)
                            {
                                int genericNameId = 0;
                                var genericNameEntry = genericNameService.GetGenericNameByName(strGN);
                                if (genericNameEntry != null)
                                {
                                    genericNameId = genericNameEntry.Id;
                                }
                                else
                                {
                                    var newGenericName = new GenericName()
                                    {
                                        TypeName = strGN,
                                        StatusFlag = 1,
                                        CreatedBy = currentUserId
                                    };
                                    genericNameId = genericNameService.Add(newGenericName);
                                }
                                var genericNameMedicineRelation = new GenericNameMedicineRelation()
                                {
                                    GenericTypeId = genericNameId,
                                    MedicineId = medicineId,
                                    StatusFlag = 1,
                                };
                                medicineService.AddGenericNameRelation(genericNameMedicineRelation);
                            }
                            //---------Generic Name------------//

                            //---------Strength------------//
                            string[] strengthChar = strStrength.Split('+');
                            foreach (string strS in strengthChar)
                            {
                                int strengthId = 0;
                                var strengthEntry = strengthService.GetStrengthByName(strS);
                                if (strengthEntry != null)
                                {
                                    strengthId = strengthEntry.Id;
                                }
                                else
                                {
                                    var newStrength = new Strength()
                                    {
                                        StrengthName = strS,
                                        StatusFlag = 1,
                                        CreatedBy = currentUserId,
                                    };
                                    strengthId = strengthService.Add(newStrength);
                                }
                                var strengthMedicineRelation = new StrengthMedicineRelation()
                                {
                                    MedicineId = medicineId,
                                    StrengthId = strengthId,
                                    StatusFlag = 1
                                };
                                medicineService.AddStrengthRelation(strengthMedicineRelation);
                            }
                            //---------Strength------------//
                            rowCount++;
                        }
                    }
                }
            }
            return rowCount + " Rows Updated";
        }

        public void UploadConfigExcel(HttpPostedFileBase excelFile)
        {
            int rowCount = 0;
            int currentUserId = AuthenticatedUser.GetUserFromIdentity().UserId;

            complaintService = new ComplaintService();
            diseaseService = new DiseaseService();
            investigationService = new InvestigationService();
            procedureService = new ProcedureService();

             HttpPostedFileBase file = excelFile;
            if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx") ||
                file.FileName.EndsWith("XLS") ||
                file.FileName.EndsWith("XLSX"))
            {
                string fileName = file.FileName;
                string fileContentType = file.ContentType;
                byte[] fileBytes = new byte[file.ContentLength];
                var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                using (var package = new ExcelPackage(file.InputStream))
                {
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet.First();
                    var noOfCol = workSheet.Dimension.End.Column;
                    var noOfRow = workSheet.Dimension.End.Row;
                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                    {
                        int colComplaint = 1;
                        int colDisease = 2;
                        int colInvestigation = 3;
                        int colSurgery = 4;
                        string strComplaint = workSheet.Cells[rowIterator, colComplaint].Value == null ? "" : workSheet.Cells[rowIterator, colComplaint].Value.ToString();
                        string strDisease = workSheet.Cells[rowIterator, colDisease].Value == null ? "" : workSheet.Cells[rowIterator, colDisease].Value.ToString();
                        string strInvestigation = workSheet.Cells[rowIterator, colInvestigation].Value == null ? "" : workSheet.Cells[rowIterator, colInvestigation].Value.ToString();
                        string strProcedure = workSheet.Cells[rowIterator, colSurgery].Value == null ? "" : workSheet.Cells[rowIterator, colSurgery].Value.ToString();

                        if (strComplaint != "" || strDisease != "" || strInvestigation != "" || strProcedure != "")
                        {
                            if (strComplaint != "" && complaintService.IsComplaintTypeExist(strComplaint, "undefined"))
                            {
                                var newComplaint = new Complaint()
                                {
                                    ComplaintType = strComplaint,
                                    StatusFlag = (byte)EnumActiveDeative.Active,
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = currentUserId
                                };
                                complaintService.Add(newComplaint);
                            }

                            if (strDisease != "" && diseaseService.IsDiseaseNameExist(strDisease, "undefined"))
                            {
                                var newDisease = new Disease()
                                {
                                    DiseaseName = strDisease,
                                    StatusFlag = (byte)EnumActiveDeative.Active,
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = currentUserId,
                                };
                                diseaseService.Add(newDisease);
                            }

                            if (strInvestigation != "" && investigationService.IsInvestigationNameExist(strInvestigation, "undefined"))
                            {
                                var newInvestigation = new Investigation()
                                {
                                    InvestigationName = strInvestigation,
                                    StatusFlag = (byte)EnumActiveDeative.Active,
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = currentUserId,
                                };
                                investigationService.Add(newInvestigation);
                            }

                            if(strProcedure != "" && procedureService.IsProcedureNameExist(strProcedure, "undefined"))
                            {
                                var newProcedure = new Procedure()
                                {
                                    ProcedureName = strProcedure,
                                    StatusFlag = (byte)EnumActiveDeative.Active,
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = currentUserId,
                                };
                                procedureService.Add(newProcedure);
                            }
                        }
                    }
                }
            }
        }
        public void Inactive()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Inactive;
            medicineService.Inactive(this);
        }
    }
}