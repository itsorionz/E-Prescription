using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPrescription.Entities;
using EPrescription.Services;


namespace EPrescription.Web.Models
{
    [NotMapped]
    public class RoleModel:Role
    {
        private RoleService _roleService { get; set; }

        public List<RoleTaskCheckBoxModel> RoleTaskList { get; set; }

        [Required]
        [Remote("IsRoleNameExist", "User", AdditionalFields = "InitialRoleName",
           ErrorMessage = "Role already Exist")]
        [Display(Name = "Role")]
        public new string RoleName
        {
            get { return base.RoleName; }
            set { base.RoleName = value; }        }

        public RoleModel()
        {

            _roleService = new RoleService();
            this.RoleTaskList = new RoleTaskCheckBoxList().TaskList.OrderBy(x => x.TaskCategory).ThenBy(x=>x.TaskName).ToList();


        }
        public List<Role> GetAllRoles()
        {
            return _roleService.GetAllRoles();
        }
         public RoleModel(int id) : this()
        {
            var role = _roleService.GetRole(id);

            this.Id = role.Id;
            this.RoleName = role.RoleName;
             this.StatusFlag = role.StatusFlag;

             this.RoleTaskList = new List<RoleTaskCheckBoxModel>();
             foreach (var item in new RoleTaskCheckBoxList().TaskList.OrderBy(x => x.TaskCategory).ThenBy(x => x.TaskName))
             {
                 item.IsChecked = role.RoleTasks.Any(x => x.Task.Equals(item.TaskName));
                 this.RoleTaskList.Add(item);
             }
        }
        public void AddRole()
        {
            var taskList = this.RoleTaskList.Where(x => x.IsChecked).Select(taskItem => new RoleTask
            {
                Task = taskItem.TaskName
            }).ToList();
            _roleService.AddRole(RoleName, taskList);
        }
        public void EditRole(int id)
        {
            var taskList = this.RoleTaskList.Where(x => x.IsChecked).Select(taskItem => new RoleTask
            {
                Task = taskItem.TaskName
            }).ToList();
            _roleService.EditRole(id, RoleName, taskList);
        }
        public bool IsRoleNameExist(string RoleName, string InitialRoleName)
        {
            return _roleService.IsRoleNameExist(RoleName, InitialRoleName);
        }
        public void Dispose()
        {
            _roleService.Dispose();
        }
    }

    public class RoleTaskCheckBoxModel
    {

        public string TaskName { get; set; }
        public string TaskCategory { get; set; }
        public bool IsChecked { get; set; } 

    }
    public class RoleTaskCheckBoxList
    {
        public  List<RoleTaskCheckBoxModel> TaskList = new List<RoleTaskCheckBoxModel>
        {
            new RoleTaskCheckBoxModel {TaskName="User_Configuration", TaskCategory = "Configuration"},
            new RoleTaskCheckBoxModel {TaskName="User_Role_Configuration", TaskCategory = "Configuration"},
          
        };


    }

}