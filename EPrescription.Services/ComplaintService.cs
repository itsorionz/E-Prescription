using EPrescription.Entities;
using EPrescription.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Services
{
    public class ComplaintService
    {
        private EPrescriptionDbContext _context;
        private ComplaintUnitOfWork complaintUnitOfWork;
        public ComplaintService()
        {
            _context = new EPrescriptionDbContext();
            complaintUnitOfWork = new ComplaintUnitOfWork(_context);
        }

        public IEnumerable<Complaint> GetAllComplaints()
        {
            return complaintUnitOfWork.ComplaintRepository.GetAll();
        }

        public void Add(Complaint complaint)
        {
            var newComplaint = new Complaint()
            {
                ComplaintType = complaint.ComplaintType,
                StatusFlag = complaint.StatusFlag,
                CreatedAt = complaint.CreatedAt,
                CreatedBy = complaint.CreatedBy,
                UpdatedAt = complaint.UpdatedAt,
                UpdatedBy = complaint.UpdatedBy
            };
            complaintUnitOfWork.ComplaintRepository.Add(newComplaint);
            complaintUnitOfWork.Save();
        }

        public Complaint GetComplaintById(int id)
        {
            return complaintUnitOfWork.ComplaintRepository.GetById(id);
        }

        public bool IsComplaintTypeExist(string complaintType, string initialComplaintType)
        {
            return complaintUnitOfWork.ComplaintRepository.IsComplaintTypeExist(complaintType, initialComplaintType);
        }

        public void Edit(Complaint complaint)
        {
            var complaintEntry = GetComplaintById(complaint.Id);
            if (complaintEntry != null)
            {
                complaintEntry.ComplaintType = complaint.ComplaintType;
                complaintEntry.UpdatedAt = complaint.UpdatedAt;
                complaintEntry.UpdatedBy = complaint.UpdatedBy;
                complaintUnitOfWork.ComplaintRepository.Update(complaintEntry);
                complaintUnitOfWork.Save();
            }
        }
    }
}
