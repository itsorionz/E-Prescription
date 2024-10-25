using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EPrescription.Entities
{
    [Table("Roles")]
    public class Role : Entity
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        
        public virtual ICollection<RoleTask> RoleTasks { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; }
        
    }    
}
