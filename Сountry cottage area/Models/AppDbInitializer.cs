using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Сountry_cottage_area.Models
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { Email = "admin@bk.ru", UserName = "admin@bk.ru", EmailConfirmed = true };
            string password = "Qwer_12345";
            var result = userManager.Create(admin, password);
            ApplicationDbContext db = new ApplicationDbContext();
            db.AgriculturesCategories.Add(new AgriculturesCategory { Name = "Овощи" });
            db.AgriculturesCategories.Add(new AgriculturesCategory { Name = "Зелень" });
            db.AgriculturesCategories.Add(new AgriculturesCategory { Name = "Ягоды" });
            db.Areas.Add(new Area { Name = admin.Email, UserId = admin.Id });
            db.SaveChanges();

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            base.Seed(context);
        }
    }
}