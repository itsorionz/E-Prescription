using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("Investigations")]
   public class Investigation:Entity
    {
        [Display(Name= "Investigation Name")]
        public string InvestigationName { get; set; }
    }
}
