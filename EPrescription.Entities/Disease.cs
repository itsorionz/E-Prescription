using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("Diseases")]
   public class Disease:Entity
    {
        [Display(Name= "Disease Name")]
        public string DiseaseName { get; set; }
    }
}
