using Microsoft.AspNetCore.Identity;
using student_testing_system.Models.Users;
using System;
using System.Threading.Tasks;

namespace student_testing_system.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeRoles(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { RoleNames.Admin, RoleNames.Teacher, RoleNames.Student };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
