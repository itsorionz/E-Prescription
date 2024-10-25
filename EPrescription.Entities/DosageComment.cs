using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("DosageComments")]
   public class DosageComment:Entity
    {
        public string CommentType { get; set; }
    }
}
