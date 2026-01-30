using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAlloraProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string[] roles = new string[] { "Admin", "Employee", "Customer" };

            for (int i = 0; i < roles.Length; i++)
            {
                if (!await roleManager.RoleExistsAsync(roles[i]))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(roles[i]));
                }
            }
        }

        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var adminUsername = "admin";
            var adminEmail = "admin@gmail.com";

            var user = await userManager.FindByNameAsync(adminUsername);

            if (user == null)
            {
                user = new User
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(user, "Admin1*");

                if (!createResult.Succeeded)
                {
                    throw new Exception(
                        $"Admin user creation failed: {string.Join(", ", createResult.Errors.Select(e => e.Description))}"
                    );
                }
            }

            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                var roleResult = await userManager.AddToRoleAsync(user, "Admin");

                if (!roleResult.Succeeded)
                {
                    throw new Exception(
                        $"Adding admin role failed: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}"
                    );
                }
            }
        }
    }
}