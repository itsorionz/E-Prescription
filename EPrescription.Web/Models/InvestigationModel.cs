using EPrescription.Common;
using EPrescription.Entities;
using EPrescription.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPrescription.Web.Models
{
    public class InvestigationModel: Investigation
    {
        private InvestigationService investigationService;
        public InvestigationModel()
        {
            investigationService = new InvestigationService();
        }

        public IEnumerable<Investigation> GetAllInvestigations()
        {
            return investigationService.GetAllInvestigations();
        }

        public void Add()
        {
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            investigationService.Add(this);
        }
    }
}