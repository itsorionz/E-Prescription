using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("Complaints")]
    public  class Complaint:Entity
    {
        [Display(Name ="Complaint Type")]
        public string ComplaintType { get; set; }
    }
}
