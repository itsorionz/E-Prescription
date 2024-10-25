using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EPrescription.Entities
{
    [Table("Users")]
    public class User:Entity
    {
        [Required]
        [Display(Name = "Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
     
        [Display(Name = "Mobile")]
        public string MobileNo { get; set; }
 
         [Display(Name = "Address")]
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool SupUser { get; set; }
      
         [Display(Name = "User Role")]
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
 
        [Display(Name = "Image Link")]
        public string ImageFile { get; set; }
        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? JoiningDate { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        public new int? CreatedBy { get; set; }
        [NotMapped]
        public new int? CreatedByUser { get; set; }
        public new int? UpdatedBy { get; set; }
        [NotMapped]
        public new int? UpdatedByUser { get; set; }

   

    }
}
