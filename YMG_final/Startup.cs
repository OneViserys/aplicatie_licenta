using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.IO;
using System.Web;
using YMG.Models;

[assembly: OwinStartupAttribute(typeof(YMG.Startup))]
namespace YMG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdminUserRoles();
        }

        private void CreateAdminUserRoles()
        {
            var ctx = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@ymgg.com",
                    FirstName = "Steve",
                    LastName = "Fergusson",
                    Day = 28,
                    Month = 3,
                    Year = 2000,
                    ProfilePicture = null
                };

                //byte[] img = File.ReadAllBytes(@"~/Images/admin_picture.png");
                user.ProfilePicture = null ;
                //user.ProfilePicture = null;

                var adminCreated = userManager.Create(user, "Admin2021!");
                if (adminCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

        }
    }
}
