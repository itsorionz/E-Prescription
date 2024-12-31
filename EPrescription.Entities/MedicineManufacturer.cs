using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Entities
{
    [Table("MedicineManufacturers")]
    public class MedicineManufacturer : Entity
    {
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public string Address { get; set; }
        [Display(Name ="Contact Number")]
        public string ContactNumber { get; set; }
        [Display(Name ="E-mail")]
        public string Email { get; set; }
    }
}
