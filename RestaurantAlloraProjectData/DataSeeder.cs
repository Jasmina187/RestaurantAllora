using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAlloraProjectData.Configurations;
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
        public static async Task SeedAllergensAsync(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<RestaurantAlloraProjectContext>();
            var seedAllergens = AllergenConfiguration.Allergens();
            var existingAllergens = await dbContext.Allergens.ToDictionaryAsync(a => a.AllergenId);
            var hasChanges = false;

            foreach (var seedAllergen in seedAllergens)
            {
                if (existingAllergens.TryGetValue(seedAllergen.AllergenId, out var existingAllergen))
                {
                    if (existingAllergen.AllergenName != seedAllergen.AllergenName)
                    {
                        existingAllergen.AllergenName = seedAllergen.AllergenName;
                        hasChanges = true;
                    }

                    continue;
                }

                dbContext.Allergens.Add(seedAllergen);
                hasChanges = true;
            }

            if (hasChanges)
            {
                await dbContext.SaveChangesAsync();
            }
        }

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

        public static async Task SeedDemoUsersAsync(IServiceProvider serviceProvider)
        {
            await SeedUserAsync(
                serviceProvider,
                userName: "denis",
                email: "denis@allora.bg",
                firstName: "Denis",
                role: "Employee",
                password: "Denis1*");

            await SeedUserAsync(
                serviceProvider,
                userName: "stoyan",
                email: "stoyan@allora.bg",
                firstName: "Stoyan",
                role: "Customer",
                password: "Stoyan1*");
        }

        private static async Task SeedUserAsync(
            IServiceProvider serviceProvider,
            string userName,
            string email,
            string firstName,
            string role,
            string password)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var dbContext = serviceProvider.GetRequiredService<RestaurantAlloraProjectContext>();

            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new User
                {
                    UserName = userName,
                    Email = email,
                    FirstName = firstName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(user, password);

                if (!createResult.Succeeded)
                {
                    throw new Exception(
                        $"{role} user creation failed: {string.Join(", ", createResult.Errors.Select(e => e.Description))}"
                    );
                }
            }

            if (!await userManager.IsInRoleAsync(user, role))
            {
                var roleResult = await userManager.AddToRoleAsync(user, role);

                if (!roleResult.Succeeded)
                {
                    throw new Exception(
                        $"Adding {role} role failed: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}"
                    );
                }
            }

            if (role == "Customer" && !await dbContext.Set<CustomerProfile>().AnyAsync(cp => cp.UserId == user.Id))
            {
                dbContext.Set<CustomerProfile>().Add(new CustomerProfile { UserId = user.Id });
            }

            if (role == "Employee" && !await dbContext.Set<EmployeeProfile>().AnyAsync(ep => ep.UserId == user.Id))
            {
                dbContext.Set<EmployeeProfile>().Add(new EmployeeProfile { UserId = user.Id });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
