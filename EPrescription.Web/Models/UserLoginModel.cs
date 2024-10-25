using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using EPrescription.Entities;
using EPrescription.Services;


namespace EPrescription.Web.Models
{
    [NotMapped]
    public class UserLoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        private UserService _userService;
        public UserLoginModel()
        {
            _userService = new UserService();
        }
        public User GetUserById(int id)
        {
            return _userService.GetUserById(id);
        }
        public User GetUserByUsername(string username)
        {
            return _userService.GetUserByUsername(username);
        }
      
        public User GetValidUserByPassword(string username, string password)
        {
            return _userService.GetValidUserByPassword(username, password);
        }
      
      
        
        public void Dispose()
        {
            _userService.Dispose();
        }
    }
    public class ChangePsswordViewModel
    {
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        private UserService _userService;
        public ChangePsswordViewModel()
        {
            _userService = new UserService();
        }
        public void SaveNewPassword(string userName, string hashNewPassword)
        {
            _userService.SaveNewPassword(userName,  hashNewPassword);
        }
        public User GetUserById(int id)
        {
            return _userService.GetUserById(id);
        }
        public User GetUserByUsername(string username)
        {
            return _userService.GetUserByUsername(username);
        }
      
        public User GetValidUserByPassword(string username, string password)
        {
            return _userService.GetValidUserByPassword(username, password);
        }
    }
    public class ResetPassViewModel
    {
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ReturnToken { get; set; }
    }
}