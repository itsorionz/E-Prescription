using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPrescription.Entities;


namespace EPrescription.Repo
{
   public class RoleRepository: Repository<Role>
   {
       private EPrescriptionDbContext _context;
       public RoleRepository(EPrescriptionDbContext context)
            :base(context)
        {
            _context = context;
        }
       public bool IsRoleNameExist(string RoleName, string InitialRoleName)
       {
           bool isNotExist = true;
           if (RoleName != string.Empty && InitialRoleName == "undefined")
           {
               var isExist = _context.Roles.Any(x => x.StatusFlag != 0 && x.RoleName.ToLower().Equals(RoleName.ToLower()));
               if (isExist)
               {
                   isNotExist = false;
               }
           }
           if (RoleName != string.Empty && InitialRoleName != "undefined")
           {
               var isExist = _context.Roles.Any(x => x.StatusFlag != 0 && x.RoleName.ToLower() == RoleName.ToLower() && x.RoleName.ToLower() != InitialRoleName.ToLower());
               if (isExist)
               {
                   isNotExist = false;
               }
           }
           return isNotExist;
       }
    }
   public class RoleTaskRepository : BaseRepository<RoleTask>
   {
       private EPrescriptionDbContext _context;
       public RoleTaskRepository(EPrescriptionDbContext context)
           : base(context)
       {
           _context = context;
       }
   }  
}
