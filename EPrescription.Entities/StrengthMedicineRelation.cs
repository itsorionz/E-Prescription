using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("StrengthMedicineRelations")]
   public class StrengthMedicineRelation:BaseEntity
    {

        [Display(Name = "Strength")]
        public int? StrengthId { get; set; }
        [ForeignKey("StrengthId")]
        public virtual Strength Strength { get; set; }

        [Display(Name = "Medicine")]
        public int? MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public virtual Medicine Medicine { get; set; }
    }
}
