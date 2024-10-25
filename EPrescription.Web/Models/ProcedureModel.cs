using EPrescription.Common;
using EPrescription.Entities;
using EPrescription.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPrescription.Web.Models
{
    public class ProcedureModel: Procedure
    {
        private ProcedureService procedureService;
        public ProcedureModel()
        {
            procedureService = new ProcedureService();
        }

        public IEnumerable<Procedure> GetAllProcedures()
        {
            return procedureService.GetAllProcedures();
        }

        public void Add()
        {
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            procedureService.Add(this);
        }
    }
}