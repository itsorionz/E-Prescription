using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPrescription.Repo;
using EPrescription.Entities;
using PagedList;
using EPrescription.Common;

namespace EPrescription.Services
{
    public class UserService
    {
        private EPrescriptionDbContext _context;
        private UserUnitOfWork _userUnitOfWork;

        public UserService()
        {
            _context = new EPrescriptionDbContext();
            _userUnitOfWork = new UserUnitOfWork(_context);
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _userUnitOfWork.UserRepository.GetAll().Where(x => x.StatusFlag != 0);
            return users.OrderBy(x => x.FullName);
        }
       
       
        public void AddUser(User user)
        {
            _userUnitOfWork.UserRepository.Add(user);
            _userUnitOfWork.Save();
        }

        public User GetUserById(int id)
        {
            return _userUnitOfWork.UserRepository.GetById(id);
        }

        public string[] GetRolesById(int id)
        {

            return _userUnitOfWork.UserRepository.GetRolesById(id);

        }

        public IPagedList<User> GetAllUserIPaged(int page, int pageSize, int? sUserRoleId, string sName)
        {
            return _userUnitOfWork.UserRepository.GetAllUserIPaged( page, pageSize,  sUserRoleId,sName);
        }

        public string[] GetRolesByUserName(string userName)
        {
            return _userUnitOfWork.UserRepository.GetRolesByUserName(userName);

        }
        public User GetUserByUsername(string username)
        {
            return _userUnitOfWork.UserRepository.GetUserByUsername(username);
        }
        public User GetValidUserByPassword(string username, string password)
        {
            return _userUnitOfWork.UserRepository.GetValidUserByPassword(username, password);
        }
        public void Delete(int id)
        {
           _userUnitOfWork.UserRepository.DeleteById(id);
            _userUnitOfWork.Save();
        }
        public void Save()
        {
            _userUnitOfWork.Save();
        }
        public bool IsUserNameExist(string UserName, string InitialUserName)
        {
            return _userUnitOfWork.UserRepository.IsUserNameExist(UserName, InitialUserName);
        }
        public bool IsEmailExist(string Email, string InitialEmail)
        {
            return _userUnitOfWork.UserRepository.IsEmailExist(Email, InitialEmail);
        }
 
        public void Dispose()
        {
            _userUnitOfWork.Dispose();
        }

        public void SaveNewPassword(string userName, string hashNewPassword)
        {
            var user = GetUserByUsername(userName);
            if (user != null)
            {
                user.Password = hashNewPassword;
                _userUnitOfWork.UserRepository.Update(user);
                _userUnitOfWork.Save();
            }
        }

        public void Active(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                user.StatusFlag = (byte)EnumActiveDeative.Active;
                _userUnitOfWork.UserRepository.Update(user);
                _userUnitOfWork.Save();
            }
        }
    }
}
