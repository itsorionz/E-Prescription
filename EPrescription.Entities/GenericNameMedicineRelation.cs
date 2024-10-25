using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("GenericNameMedicineRelations")]
   public class GenericNameMedicineRelation:BaseEntity
    {
        [Display(Name = "Generic Name")]
        public int? GenericTypeId { get; set; }
        [ForeignKey("GenericTypeId")]
        public virtual GenericName GenericName { get; set; }

        [Display(Name = "Medicine")]
        public int? MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public virtual Medicine Medicine { get; set; }
    }
}
