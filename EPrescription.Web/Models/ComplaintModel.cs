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
    public class ComplaintModel: Complaint
    {
        private ComplaintService complaintService;
        [Required]
        [Remote("IsComplaintTypeExist", "Complaint", AdditionalFields = "InitialComplaintType",
            ErrorMessage = "Complaint Type already Exist")]
        [Display(Name = "Complaint Type")]
        public new string ComplaintType
        {
            get { return base.ComplaintType; }
            set { base.ComplaintType = value; }
        }

        public ComplaintModel()
        {
            complaintService = new ComplaintService();
        }

        public ComplaintModel(int id):this()
        {
            var complaintEntry = complaintService.GetComplaintById(id);
            if (complaintEntry != null)
            {
                base.Id = complaintEntry.Id;
                base.ComplaintType = complaintEntry.ComplaintType;
                base.CreatedAt = complaintEntry.CreatedAt;
                base.StatusFlag = complaintEntry.StatusFlag;
                base.UpdatedAt = complaintEntry.UpdatedAt;
                base.UpdatedBy = complaintEntry.UpdatedBy;
                base.CreatedBy = complaintEntry.CreatedBy;
            }
        }

        public void Edit()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            complaintService.Edit(this);
        }

        public IEnumerable<Complaint> GetAllComplaints()
        {
            return complaintService.GetAllComplaints().Where(s => s.StatusFlag == 1);
        }

        public void Add()
        {
            base.CreatedAt = DateTime.Now;
            base.UpdatedAt = DateTime.Now;
            base.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Active;
            complaintService.Add(this);
        }
        public bool IsComplaintTypeExist(string complaintType, string initialComplaintType)
        {
            return complaintService.IsComplaintTypeExist(complaintType, initialComplaintType);
        }
        public void Inactive()
        {
            base.UpdatedAt = DateTime.Now;
            base.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            base.StatusFlag = (byte)EnumActiveDeative.Inactive;
            complaintService.Inactive(this);
        }
    }
}