using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EPrescription.Services;
using EPrescription.Entities;
using EPrescription.Web.Models;
using Microsoft.AspNet.Identity;
using System.Text;
using PagedList;

namespace EPrescription.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        [Roles("Global_SupAdmin,User_Configuration")]
        public ActionResult Index(UserSearchModel model)
        {
            model.UserPagedList = new UserModel().GetAllUserIPaged(model.Page, model.PageSize, model.SUserRoleId, model.SName);
            return View(model);
        }
        #region login logout area
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                TempData["PasswordAgedMessage"] = "";

                var loginModel = new UserLoginModel();
                // find user by username first
                var user = loginModel.GetUserByUsername(model.UserName);
                if (user != null)
                {
                    MD5 md5Hash = MD5.Create();
                    string hashPassword = GetMd5Hash(md5Hash, model.Password);
                    var validUser = loginModel.GetValidUserByPassword(model.UserName, hashPassword);
                    if (validUser == null)
                    {
                        ModelState.AddModelError("", "Invalid credentials. Please try again.");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(validUser.Id + "|" + validUser.UserName.ToUpper() + "|" + validUser.FullName + "|" + validUser.ImageFile, model.RememberMe); ;
                        Session["sessionid"] = System.Web.HttpContext.Current.Session.SessionID;
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                ModelState.Remove("Password");
            }
            // If we got this far, something failed, redisplay form


            return View(model);
        }
        [Authorize]
        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }
        #endregion
        #region user register area

        [Roles("Global_SupAdmin,User_Configuration")]
        public ActionResult Register()
        {
            ViewBag.Message = "";
            TempData["RegistrationSuccess"] = "";
            UserModel registerModel = new UserModel();
            return View(registerModel);
        }
        [Roles("Global_SupAdmin,User_Configuration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserModel userRegister)
        {
            ViewBag.Message = "";
            if (ModelState.IsValid)
            {

                userRegister.AddUser();
                TempData["RegistrationSuccess"] = "New user registration successfully complete! Username and Password sent to user by Email.";

                return RedirectToAction("index");
            }
            else
            {
                ViewBag.Message = "Something went wrong! please try again";
            }
            //ViewBag.RoleId = new SelectList(db.Roles.Where(r => r.RoleId != 1 && r.Status == 1).OrderBy(x => x.RoleName), "RoleId", "RoleName", userRegister.RoleId);
            return View(userRegister);
        }
        #endregion

        #region user edit area
        [Roles("Global_SupAdmin,User_Configuration")]
        public ActionResult Edit(int id)
        {

            ViewBag.Message = "";
            TempData["RegistrationSuccess"] = "";
            UserModel registerModel = new UserModel(id);

            return View(registerModel);
        }
        [Roles("Global_SupAdmin,User_Configuration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserModel userRegister)
        {
            ViewBag.Message = "";
            userRegister.EditUser();
            TempData["RegistrationSuccess"] = "User update successfully complete!";
            return RedirectToAction("index");
        }
        #endregion
        #region user password change area
        [AllowAnonymous]
        public ActionResult ChangePassword(string userName)
        {
            ViewBag.PasswordAged = "";
            ViewBag.oldPasswordNotMatched = "";
            ViewBag.PasswordHistryAlert = "";
            ChangePsswordViewModel model = new ChangePsswordViewModel();
            if (userName != null)
            {
                model.UserName = userName;
            }
            else
            {
                model.UserName = AuthenticatedUser.GetUserFromIdentity().Username;
            }
            ViewBag.PasswordAged = TempData["PasswordAgedMessage"];
            return View(model);
        }
        // POST: /Account/ChangePassword
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ChangePassword(ChangePsswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    if (model.NewPassword == model.OldPassword)
                    {
                        ViewBag.PasswordHistryAlert = "Both password can not be same";
                        changePasswordSucceeded = false;
                    }
                    else
                    {
                        MD5 md5Hash = MD5.Create();
                        string hashOldPassword = GetMd5Hash(md5Hash, model.OldPassword);
                        string hashNewPassword = GetMd5Hash(md5Hash, model.NewPassword);
                        var user = model.GetValidUserByPassword(model.UserName, hashOldPassword);
                        model.SaveNewPassword(model.UserName, hashNewPassword);
                        changePasswordSucceeded = true;

                    }
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded == true)
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return RedirectToAction("ChangePasswordSuccess", "User");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Current password is incorrect or new password is invalid.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
        #endregion
        #region user roles configuration

        [Roles("Global_SupAdmin,User_Role_Configuration")]
        public ActionResult Roles()
        {
            var roles = new RoleModel().GetAllRoles().ToList();
            return View(roles);
        }
        [Roles("Global_SupAdmin,User_Role_Configuration")]
        public ActionResult AddRole()
        {
            var roleModel = new RoleModel();

            return View(roleModel);
        }
        [Roles("Global_SupAdmin,User_Role_Configuration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRole(RoleModel roleModel)
        {
            try
            {
                roleModel.AddRole();
                TempData["message"] = "Successfully added Role.";
                TempData["alertType"] = "success";
            }

            catch (Exception e)
            {
                TempData["message"] = "Failed to Add Role.";
                TempData["alertType"] = "danger";
                Console.Write(e.Message);
            }

            return RedirectToAction("Roles");
        }
        [Roles("Global_SupAdmin,User_Role_Configuration")]
        public ActionResult EditRole(int id)
        {

            var role = new RoleModel(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }
        [Roles("Global_SupAdmin,User_Role_Configuration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RoleModel model)
        {
            model.EditRole(model.Id);
            return RedirectToAction("Roles");
        }
        #endregion


        public JsonResult IsUserNameExist(string UserName, string InitialUserName)
        {
            bool isNotExist = new UserModel().IsUserNameExist(UserName, InitialUserName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsEmailExist(string Email, string InitialEmail)
        {
            bool isNotExist = new UserModel().IsEmailExist(Email, InitialEmail);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsRoleNameExist(string RoleName, string InitialRoleName)
        {
            bool isNotExist = new RoleModel().IsRoleNameExist(RoleName, InitialRoleName);
            return Json(isNotExist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            new UserModel().Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Active(int id)
        {
            new UserModel().Active(id);
            return RedirectToAction("Index");
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






    }
}