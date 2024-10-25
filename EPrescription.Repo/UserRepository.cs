using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPrescription.Entities;
using EPrescription.Common;
using PagedList;

namespace EPrescription.Repo
{
    public class UserRepository : Repository<User>
    {
        private EPrescriptionDbContext _context;
        public UserRepository(EPrescriptionDbContext context)
            : base(context)
        {
            _context = context;
        }
        public bool IsUserNameExist(string UserName, string InitialUserName)
        {
            bool isNotExist = true;
            if (UserName != string.Empty && InitialUserName == "undefined")
            {
                var isExist = _context.Users.Any(x => x.StatusFlag !=(byte)EnumActiveDeative.Inactive && x.UserName.ToLower().Equals(UserName.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (UserName != string.Empty && InitialUserName != "undefined")
            {
                var isExist = _context.Users.Any(x => x.StatusFlag != (byte)EnumActiveDeative.Inactive && x.UserName.ToLower() == UserName.ToLower() && x.UserName.ToLower() != InitialUserName.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }

        public IPagedList<User> GetAllUserIPaged(int page, int pageSize, int? sUserRoleId, string sName)
        {
            return _context.Users.Where(s => ((sUserRoleId == null) || s.RoleId == sUserRoleId) || ((sName == null) || (s.FullName.ToUpper().Contains(sName.ToUpper()) || s.UserName.ToUpper().Contains(sName.ToUpper())))).OrderBy(o=>o.FullName).ToPagedList(page, pageSize);
        }

        public bool IsEmailExist(string Email, string InitialEmail)
        {
            bool isNotExist = true;
            if (Email != string.Empty && InitialEmail == "undefined")
            {
                var isExist = _context.Users.Any(x => x.StatusFlag != (byte)EnumActiveDeative.Inactive && x.Email.ToLower().Equals(Email.ToLower()));
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            if (Email != string.Empty && InitialEmail != "undefined")
            {
                var isExist = _context.Users.Any(x => x.StatusFlag != (byte)EnumActiveDeative.Inactive && x.Email.ToLower() == Email.ToLower() && x.Email.ToLower() != InitialEmail.ToLower());
                if (isExist)
                {
                    isNotExist = false;
                }
            }
            return isNotExist;
        }
    
        public User GetUserByUsername(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.ToUpper() == username.ToUpper() && u.StatusFlag != 0);
            return user;

        }
        public User GetValidUserByPassword(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName.ToUpper() == username.ToUpper() && u.StatusFlag != 0 && u.Password.Equals(password));
            return user;

        }
    
        
        public string[] GetRolesById(int id)
        {
            string[] roleTasks = (from a in _context.RoleTasks
                                  join b in _context.Users on a.RoleId equals b.RoleId
                                  where b.Id.Equals(id)
                                  select a.Task).ToArray<string>();
            return roleTasks;

        }
        public string[] GetRolesByUserName(string userName)
        {
            string[] roleTasks = (from a in _context.RoleTasks
                                  join b in _context.Users on a.RoleId equals b.RoleId
                                  where b.UserName.Equals(userName)
                                  select a.Task).ToArray<string>();
            return roleTasks;

        }
    }
}
