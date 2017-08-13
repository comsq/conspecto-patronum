using ConspectoPatronum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConspectoPatronum.Startup))]
namespace ConspectoPatronum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        public void CreateRoles()
        {
            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("Administrator"))
            {
                var adminRole = new IdentityRole();
                adminRole.Name = "Administrator";
                roleManager.Create(adminRole);

                var user = new ApplicationUser();
                user.Email = "semenmakhaev@yandex.ru";
                var pwd = "123456#1";
                var check = userManager.Create(user, pwd);
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                }
            }
        }
    }
}
