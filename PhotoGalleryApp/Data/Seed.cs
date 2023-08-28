using Microsoft.AspNetCore.Identity;
using PhotoGalleryApp.Models;
using System.Net;

namespace PhotoGalleryApp.Data
{
    public class Seed
    {
        // Seed admin credentials
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(Roles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(Roles.Admin));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppAdmin>>();
                string adminUserEmail = "miro173928@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppAdmin()
                    {
                        UserName = "Admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "Nbs1234?");
                    await userManager.AddToRoleAsync(newAdminUser, Roles.Admin);
                }            
                
            }
        }
    }
}
