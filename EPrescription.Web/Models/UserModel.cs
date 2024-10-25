using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using EPrescription.Entities;
using EPrescription.Services;
using PagedList;
using EPrescription.Common;
using System.Text;

namespace EPrescription.Web.Models
{
   [NotMapped]
    public class UserModel:User
    {
        private UserService _userManagementService;
        private RoleService _roleService;
        public List<Role> RoleList { get; set; }
        [Display(Name ="Image")]
        public HttpPostedFileBase ImageFileBase { get; set; }
        [Required]
         [Remote("IsUserNameExist", "User", AdditionalFields = "InitialUserName",
           ErrorMessage = "User already Exist")]
         [Display(Name = "Username")]
         public new string UserName
         {
             get { return base.UserName; } 
             set { base.UserName = value; } 
         }
        [Display(Name ="Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage="Password Not Match")]
        public string ConfirmPassword { get; set; }
        public IPagedList<User> GetAllUserIPaged(int page, int pageSize, int? sUserRoleId, string sName)
        {
            return _userManagementService.GetAllUserIPaged(page, pageSize, sUserRoleId, sName);
        }

         [Display(Name = "Email")]
         [Remote("IsEmailExist", "User", AdditionalFields = "InitialEmail",
           ErrorMessage = "Email already Exist")]
         public new string Email
         {
             get { return base.Email; }
             set { base.Email = value; }
         }
       

        public UserModel()
        {
            _roleService = new RoleService();
            RoleList = _roleService.GetAllRoles();
            _userManagementService = new UserService();
          
        }
        
        public UserModel(int id) :
            this()
        {
            var userEntity = _userManagementService.GetUserById(id);
            Id = userEntity.Id;
            FullName = userEntity.FullName;
            MobileNo = userEntity.MobileNo;
            UserName = userEntity.UserName;
            Email = userEntity.Email;;
            Address = userEntity.Address;
            Gender = userEntity.Gender;
            SupUser = userEntity.SupUser;
            RoleId = userEntity.RoleId;
            Role = userEntity.Role;
            ImageFile = userEntity.ImageFile;
            StatusFlag = userEntity.StatusFlag;
            Address = userEntity.Address;
            JoiningDate = userEntity.JoiningDate;
        }

        public IEnumerable<User> GetAllUser()
        {
            return _userManagementService.GetAllUsers();
        }
      
        public void AddUser()
        {
            MD5 md5Hash = MD5.Create();
            string finalFileName = null;
            if (ImageFileBase!= null)
            {
                var fileNameWithoutExt = Path.GetFileNameWithoutExtension(ImageFileBase.FileName);
                var fileExtension = Path.GetExtension(ImageFileBase.FileName);
               finalFileName = fileNameWithoutExt + "_" + string.Format("{0:yyMMddhhmmss}", DateTime.Now) + fileExtension;
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), finalFileName);
                ImageFileBase.SaveAs(path);
                
            }
            var user = new User();
            
            user.FullName = FullName;
            user.MobileNo = MobileNo;
            user.UserName = UserName;
            user.Email = Email;
            user.Address =Address;
            user.Gender = Gender;
            user.SupUser = SupUser;
            user.StatusFlag = (byte)EnumActiveDeative.Active;
            user.CreatedAt = DateTime.Now;
            user.JoiningDate = JoiningDate;
            user.UpdatedAt = DateTime.Now;
            user.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            user.CreatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            user.RoleId = RoleId;
            if (!string.IsNullOrEmpty(finalFileName))
            {
                user.ImageFile = "/Images/" + finalFileName;
            }
            user.Password = GetMd5Hash(md5Hash, Password);
            _userManagementService.AddUser(user);
        }

        public void EditUser()
        {
            string finalFileName = null;
            if (ImageFileBase != null)
            {
                var fileNameWithoutExt = Path.GetFileNameWithoutExtension(ImageFileBase.FileName);
                var fileExtension = Path.GetExtension(ImageFileBase.FileName);
                finalFileName = fileNameWithoutExt + "_" + string.Format("{0:yyMMddhhmmss}", DateTime.Now) + fileExtension;
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), finalFileName);
                ImageFileBase.SaveAs(path);
            }
            var userEntity = _userManagementService.GetUserById(Id);
            userEntity.FullName = FullName;
            userEntity.MobileNo = MobileNo;
            userEntity.UserName = UserName;
            userEntity.Email = Email;
            userEntity.Address = Address;
            userEntity.Gender = Gender;
            userEntity.UpdatedAt = DateTime.Now;
            userEntity.RoleId = RoleId;         
            userEntity.UpdatedAt = DateTime.Now;
            userEntity.UpdatedBy = AuthenticatedUser.GetUserFromIdentity().UserId;
            userEntity.JoiningDate = JoiningDate;
            if (!string.IsNullOrEmpty(finalFileName))
            {
                userEntity.ImageFile = "/Images/" + finalFileName;
            }
            _userManagementService.Save();
        }
        public User GetUserById(int id)
        {
            return _userManagementService.GetUserById(id);
        }
        public User GetUserByUsername(string username)
        {
            return _userManagementService.GetUserByUsername(username);
        }

        public User GetValidUserByPassword(string username, string password)
        {
            return _userManagementService.GetValidUserByPassword(username,password);
        }
        public bool UserAuthenticationValid(string username)
        {
            bool checkValid=false;
            var user= _userManagementService.GetUserByUsername(username);
            if (user!=null)
            {
                checkValid = true;
            }
            return checkValid;
        }
        public void Delete(int id)
        {
            _userManagementService.Delete(id);
        }
        //Get authenticaiton user from file for development environment
        public string GetUsernameFromFile(string filename)
        {
            string fileName = System.Web.HttpContext.Current.Server.MapPath("/" + filename + ".txt");
            string str = string.Empty;
            if (System.IO.File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    //string line1 = sr.ReadLine();
                    string line;
                    // string[] line1Array = line1.Split("//".ToCharArray(), 2);
                    // str = line1Array[0].ToUpper();

                    while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                    {

                        if (!line.StartsWith("//"))
                        {
                            string[] line1Array = line.Split("//".ToCharArray(), 2);
                            str = line1Array[0].ToUpper();
                            break;
                        }
                    }
                }
            }
            else
            {
                str = "File does not exists";
            }
            return str;
        }
        public bool IsUserNameExist(string UserName, string InitialUserName)
        {
            return _userManagementService.IsUserNameExist(UserName, InitialUserName);
        }
        public bool IsEmailExist(string Email, string InitialEmail)
        {
            return _userManagementService.IsEmailExist(Email, InitialEmail);
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public void Dispose()
        {
            _userManagementService.Dispose();
        }

        public void Active(int id)
        {
            _userManagementService.Active(id);
        }
    }
}