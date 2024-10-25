using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("DosageTypes")]
    public class DosageType : Entity
    {
        [Display(Name ="Type Name")]
        public string TypeName { get; set; }
    }
}
