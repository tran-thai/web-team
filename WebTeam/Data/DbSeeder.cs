﻿using Microsoft.AspNetCore.Identity;
using WebTeam.Constants;

namespace WebTeam.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Student.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Marketing_manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Marketing_Coordinator.ToString()));

            // creating admin

            var user = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Ravindra",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }

            // Student 
            var user1 = new ApplicationUser
            {
                UserName = "student@gmail.com",
                Email = "student@gmail.com",
                Name = "Ravindra",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var userInDb1 = await userManager.FindByEmailAsync(user1.Email);
            if (userInDb1 == null)
            {
                await userManager.CreateAsync(user1, "Admin@123");
                await userManager.AddToRoleAsync(user1, Roles.Student.ToString());
            }
        }

    }
}
