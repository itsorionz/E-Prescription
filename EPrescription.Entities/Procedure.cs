using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("Procedures")]
    public  class Procedure:Entity
    {
        [Display(Name = "Procedure Name")]
        public string ProcedureName { get; set; }
    }
}
