using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("DosageTypeMedicineRelations")]
   public class DosageTypeMedicineRelation:BaseEntity
    {
        [Display(Name = "Dosage Type")]
        public int? DosageTypeId { get; set; }
        [ForeignKey("DosageTypeId")]
        public virtual DosageType DosageType { get; set; }

        [Display(Name = "Medicine")]
        public int? MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public virtual Medicine Medicine { get; set; }
    }
}
