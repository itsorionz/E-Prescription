using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("Strengths")]
   public class Strength:Entity
    {
        [Display(Name = "Strength Name")]
        public string StrengthName { get; set; }
    }
}
