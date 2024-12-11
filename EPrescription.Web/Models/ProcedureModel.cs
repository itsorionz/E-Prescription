using Antlr.Runtime.Misc;
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
    public class ProcedureModel: Procedure
    {
        private ProcedureService procedureService;
        [Required]
        [Remote("IsProcedureNameExist", "Procedure", AdditionalFields = "InitialProcedureName", ErrorMessage = "ProcedureName already Exist")]
        [Display(Name = "Procedure Name")]
        public new string ProcedureName
        {
            get { return base.ProcedureName; }
            set { base.ProcedureName = value; }
        }
        public ProcedureModel()
        {
            procedureService = new ProcedureService();
        }
        public IEnumerable<Procedure> GetAllProcedures()
        {
            return procedureService.GetAllProcedures().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        }
        public ProcedureModel(int id) : this()
        {
            var procedureEntry = procedureService.GetProceduretById(id);
            if (procedureEntry != null)
            {
                base.Id = procedureEntry.Id;
                base.ProcedureName = procedureEntry.ProcedureName;
                base.CreatedAt = procedureEntry.CreatedAt;
                base.StatusFlag = procedureEntry.StatusFlag;
                base.UpdatedAt = procedureEntry.UpdatedAt;
                base.UpdatedBy = procedureEntry.UpdatedBy;
                base.CreatedBy = procedureEntry.CreatedBy;
            }
        }
        public void Add()
        {
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            procedureService.Add(this);
        }
        public bool IsProcedureNameExist(string ProcedureName, string InitialProcedureName)
        {
            return procedureService.IsProcedureNameExist(ProcedureName, InitialProcedureName);
        }

        public void Edit()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            procedureService.Edit(this);
        }

        public void Inactive()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Inactive;
            procedureService.Inactive(this);
        }
    }
}