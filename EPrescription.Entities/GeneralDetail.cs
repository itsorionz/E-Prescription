using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("GeneralDetails")]
   public class GeneralDetail:BaseEntity
    {

        public float? Height { get; set; }
        public float? Weigth { get; set; }
        public int? Temp { get; set; }
        public int? Pulse { get; set; }
        public int? BP { get; set; }
        public bool Smoker { get; set; }
        public bool Allergy { get; set; }
    }
}
