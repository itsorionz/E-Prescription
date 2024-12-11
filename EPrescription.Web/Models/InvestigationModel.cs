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
    public class InvestigationModel: Investigation
    {
        private InvestigationService investigationService;

        [Required]
        [Remote("IsInvestigationNameExist", "Investigation", AdditionalFields = "InitialInvestigationName", ErrorMessage = "Investigation Name already Exist")]
        [Display(Name = "Investigation Name")]
        public new string InvestigationName
        {
            get { return base.InvestigationName; }
            set { base.InvestigationName = value; }
        }

        public InvestigationModel()
        {
            investigationService = new InvestigationService();
        }

        public InvestigationModel(int id) : this()
        {
            var investigationEntry = investigationService.GetInvestigationById(id);
            if (investigationEntry != null)
            {
                base.Id = investigationEntry.Id;
                base.InvestigationName = investigationEntry.InvestigationName;
                base.CreatedAt = investigationEntry.CreatedAt;
                base.StatusFlag = investigationEntry.StatusFlag;
                base.UpdatedAt = investigationEntry.UpdatedAt;
                base.UpdatedBy = investigationEntry.UpdatedBy;
                base.CreatedBy = investigationEntry.CreatedBy;
            }
        }

        public IEnumerable<Investigation> GetAllInvestigations()
        {
            return investigationService.GetAllInvestigations().Where(s => s.StatusFlag == (byte)EnumActiveDeative.Active);
        }

        public void Add()
        {
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            investigationService.Add(this);
        }

        public void Edit()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            investigationService.Edit(this);
        }

        public bool IsInvestigationNameExist(string InvestigationModel, string initialInvestigationModel)
        {
            return investigationService.IsInvestigationNameExist(InvestigationModel, initialInvestigationModel);
        }
        public void Inactive()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Inactive;
            investigationService.Inactive(this);
        }
    }
}