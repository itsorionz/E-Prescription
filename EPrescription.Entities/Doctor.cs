using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("Doctors")]
   public class Doctor:Entity
    {
        public string Name { get; set; }
        public string Degree { get; set; }
        public string Specialty { get; set; }
        public string Institute { get; set; }
        public string RegNo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

    }
}
