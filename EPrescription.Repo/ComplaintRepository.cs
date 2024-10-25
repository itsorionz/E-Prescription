using EPrescription.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Repo
{
   public class ComplaintRepository:Repository<Complaint>
    {
        private EPrescriptionDbContext _context;
        public ComplaintRepository(EPrescriptionDbContext context):base(context)
        {
            _context = context;
        }

        public bool IsComplaintTypeExist(string complaintType, string initialComplaintType)
        {
            bool isNotExist = true;
            if (complaintType != string.Empty && initialComplaintType == "undefined")
            {
                var isExist = _context.Complaints.Any(x => x.ComplaintType.ToLower().Equals(complaintType.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (complaintType != string.Empty && initialComplaintType != "undefined")
            {
                var isExist = _context.Complaints.Any(x =>  x.ComplaintType.ToLower() == complaintType.ToLower() && x.ComplaintType.ToLower() != initialComplaintType.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }
    }
}
