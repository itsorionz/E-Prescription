using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("PatientComplaints")]
    public class PatientComplaint : BaseEntity
    {
       
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        public string Complaint { get; set; }
        public string Remarks { get; set; }
    }
}
