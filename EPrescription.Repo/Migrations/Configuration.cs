namespace EPrescription.Repo.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EPrescription.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<EPrescription.Repo.EPrescriptionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EPrescription.Repo.EPrescriptionDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //Seed Data
            //-------------

            //var seedRoles = new List<Role>
            //{
            //    new Role {RoleName = "Administrator", StatusFlag = 2},
            //    new Role {RoleName = "Admin", StatusFlag = 1},
            //};
            //seedRoles.ForEach(s => context.Roles.AddOrUpdate(r => r.RoleName, s));
            //context.SaveChanges();
            //var seedRolePermissionList = new List<RoleTask>
            //{
            //    new RoleTask{RoleId = context.Roles.FirstOrDefault(x => x.RoleName == "Administrator").Id, Task = "Global_SupAdmin"},
            //};
            //seedRolePermissionList.ForEach(s => context.RoleTasks.AddOrUpdate(u => u.Task, s));
            //context.SaveChanges();

            //var users = new List<User>
            //{
            //    new User { FullName="Administrator",UserName = "admin",Password = "827ccb0eea8a706c4c34a16891f84e7b",RoleId = context.Roles.FirstOrDefault(x => x.RoleName == "Administrator").Id,SupUser = true, StatusFlag=2},//12345
            //    new User { FullName="Development",UserName = "dev",Password = "827ccb0eea8a706c4c34a16891f84e7b",RoleId = context.Roles.FirstOrDefault(x => x.RoleName == "Administrator").Id,SupUser = true, StatusFlag=2}, //12345
            //};
            //users.ForEach(s => context.Users.AddOrUpdate(u => u.UserName, s));
            //context.SaveChanges();

        }
    }
}
