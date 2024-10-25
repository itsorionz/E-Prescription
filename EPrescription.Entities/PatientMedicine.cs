using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("PatientMedicines")]
   public class PatientMedicine:BaseEntity
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [Display(Name = "Medicine")]
        public string Medicine { get; set; }
        [Display(Name = "Generic Name")]
        public string GenericName { get; set; }

        [Display(Name = "Dosage")]
        public string Dosage { get; set; }
        [Display(Name = "Dosage Comment")]
        public string DosageComment { get; set; }
        [Display(Name = "Duration")]
        public int Duration { get; set; }
        public string DurationUnit { get; set; }
        public string Avaiablity { get; set; }

         
    }
}
