using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StudentCourseAPI.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new[] { "Admin", "User", "Instructor" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}