using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("GenericNames")]
   public class GenericName:Entity
    {
        [Display(Name ="Type Name")]
        public string TypeName { get; set; }
    }
}
