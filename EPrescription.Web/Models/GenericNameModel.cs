using EPrescription.Common;
using EPrescription.Entities;
using EPrescription.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPrescription.Web.Models
{
    public class GenericNameModel:GenericName
    {
        private GenericNameService genericNameService;
        public GenericNameModel()
        {
            genericNameService = new GenericNameService();
        }

        public List<GenericName> GetAllGenericNames()
        {
            return genericNameService.GetAllGenericNames().ToList();
        }

        public void Add()
        {
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            genericNameService.Add(this);
        }
    }
}