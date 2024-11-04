using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("Patients")]
   public class Patient:Entity
    {
        [Display(Name = "Patient No")]
        public string PatientNo { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Age { get; set; }
        [Display(Name= "Date Of Birth")]
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
        public string Comments { get; set; }

        public int GeneralDetailId { get; set; }
        [ForeignKey("GeneralDetailId")]
        public virtual GeneralDetail GeneralDetail { get; set; }
        public virtual ICollection<PatientComplaint> PatientComplaints { get; set; }
        public virtual ICollection<PatientDisease> PatientDiseases { get; set; }
        public virtual ICollection<PatientMedicine> PatientMedicines { get; set; }
        public virtual ICollection<PatientProcedure> PatientProcedures { get; set; }
        public virtual ICollection<PatientInvestigation> PatientInvestigations { get; set; }
    }
}
