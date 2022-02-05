using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebStore.Models;

[assembly: OwinStartupAttribute(typeof(WebStore.Startup))]
namespace WebStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            CreateRoles();

            CreateUsers();
        }

        private void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            // creating Creating Manager role     
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

            }

            // creating Creating Manager role     
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

            }

            // creating Creating Employee role     
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }

            // creating Creating Employee role     
            if (!roleManager.RoleExists("Cashier"))
            {
                var role = new IdentityRole();
                role.Name = "Cashier";
                roleManager.Create(role);

            }

            // creating Creating Employee role     
            if (!roleManager.RoleExists("Accountant"))
            {
                var role = new IdentityRole();
                role.Name = "Accountant";
                roleManager.Create(role);

            }

            // creating Creating Agent role     
            if (!roleManager.RoleExists("Agent"))
            {
                var role = new IdentityRole();
                role.Name = "Agent";
                roleManager.Create(role);

            }



        }

        private void CreateUsers()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var admin = UserManager.FindByEmail("admin@gmail.com");

                if (admin == null)
                {
                    var user = new ApplicationUser();

                    user.UserName = "admin@gmail.com";

                    user.Email = "admin@gmail.com";

                    user.PhoneNumber = "0704509484";

                    user.EmailConfirmed = true;

                    user.FirstName = "Peter";

                    user.LastName = "Kachezi";

                    user.CreateDate = DateTime.Now;

                    user.AccountStatus = 1;

                    string userPWD = "Admin@2021";

                    var chkUser = UserManager.Create(user, userPWD);

                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "Admin");
                    }

                }

                var cashier = UserManager.FindByEmail("cashier@gmail.com");

                if (cashier == null)
                {
                    var user = new ApplicationUser();

                    user.UserName = "cashier@gmail.com";

                    user.Email = "cashier@gmail.com";

                    user.PhoneNumber = "0704509484";

                    user.EmailConfirmed = true;

                    user.FirstName = "Mr";

                    user.LastName = "Cashier";

                    user.CreateDate = DateTime.Now;

                    user.AccountStatus = 1;

                    string userPWD = "Cashier@2021";

                    var chkUser = UserManager.Create(user, userPWD);

                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "Cashier");

                    }

                }

                var agent = UserManager.FindByEmail("agent@gmail.com");

                if (agent == null)
                {
                    var user = new ApplicationUser();

                    user.UserName = "agent@gmail.com";

                    user.Email = "agent@gmail.com";

                    user.PhoneNumber = "0704509484";

                    user.EmailConfirmed = true;

                    user.FirstName = "Peter";

                    user.LastName = "Kachezi";

                    user.CreateDate = DateTime.Now;

                    user.AccountStatus = 1;

                    string userPWD = "Agent@2021";

                    var chkUser = UserManager.Create(user, userPWD);

                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, "Agent");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                             

            }

        }
    }
}
