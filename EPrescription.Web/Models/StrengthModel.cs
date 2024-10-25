using EPrescription.Common;
using EPrescription.Entities;
using EPrescription.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPrescription.Web.Models
{
    public class StrengthModel: Strength
    {
        private StrengthService strengthService;
        public StrengthModel()
        {
            strengthService = new StrengthService();
        }

        public IEnumerable<Strength> GetAllStrengths()
        {
            return strengthService.GetAllStrength();
        }

        public void Add()
        {
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            strengthService.Add(this);
        }
    }
}