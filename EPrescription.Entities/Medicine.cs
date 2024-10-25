using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("Medicines")]
   public class Medicine:Entity
    {
        [Display(Name="Manufacturer")]
        public int? MedicineManufacturerId { get; set; }
        [ForeignKey("MedicineManufacturerId")]
        public virtual MedicineManufacturer MedicineManufacturer { get; set; }
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [Display(Name = "Use For")]
        public string UseFor { get; set; }

        [Display(Name = "DAR")]
        public string Dar { get; set; }
        public virtual ICollection<DosageTypeMedicineRelation> DosageTypeMedicineRelations { get; set; }
        public virtual ICollection<GenericNameMedicineRelation> GenericNameMedicineRelations { get; set; }
        public virtual ICollection<StrengthMedicineRelation> StrengthMedicineRelations { get; set; }
    }
}
