using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPrescription.Entities;
using EPrescription.Repo;


namespace EPrescription.Services
{
    public class RoleService
    {
        private EPrescriptionDbContext _context;
        private RoleUnitOfWork _roleUnitOfWork;
        private RoleTaskUnitOfWork _roleTaskUnitOfWork;

        public RoleService()
        {
            _context = new EPrescriptionDbContext();
            _roleUnitOfWork = new RoleUnitOfWork(_context);
            _roleTaskUnitOfWork = new RoleTaskUnitOfWork(_context);

        }

        public List<Role> GetAllRoles()
        {
            return _roleUnitOfWork.RoleRepository.GetAll().Where(x => x.StatusFlag == 1).ToList();
        }

        public Role GetRole(int id)
        {
            return _roleUnitOfWork.RoleRepository.GetById(id);
        }

        public void DeleteRole(int id)
        {
            _roleUnitOfWork.RoleRepository.DeleteById(id);
            _roleUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _roleUnitOfWork.RoleRepository.GetAll().Count();

        }
        public void AddRole(string roleName, List<RoleTask> roleTaskList)
        {
            
            var role = new Role()
            {
                RoleName = roleName,
                StatusFlag = 1,
                RoleTasks = roleTaskList
            };
            
           // role.RoleTasks = roleTaskList;
            _roleUnitOfWork.RoleRepository.Add(role);
            _roleUnitOfWork.Save();
           
        }
        public void EditRole(int id, string roleName, List<RoleTask> roleTaskList)
        {
            var role = _roleUnitOfWork.RoleRepository.GetById(id);
            role.RoleName = roleName;
            var roleTasks = role.RoleTasks.ToList();
            foreach (var removePermission in roleTasks)
            {
                _roleTaskUnitOfWork.RoleTaskRepository.DeleteFromDatabaseByItem(removePermission);
            }
            _roleTaskUnitOfWork.Save();
            role.RoleTasks = roleTaskList;
            _roleUnitOfWork.Save();
            
        }
        public bool IsRoleNameExist(string RoleName, string InitialRoleName)
        {
            return _roleUnitOfWork.RoleRepository.IsRoleNameExist(RoleName, InitialRoleName);
        }
        public void Dispose()
        {
            _roleUnitOfWork.Dispose();
        }
    }
   
   
}
