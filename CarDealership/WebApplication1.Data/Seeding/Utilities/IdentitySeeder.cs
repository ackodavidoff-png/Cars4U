using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.Models;
using WebApplication1.Services.Data.AdminUser.Request;
using static WebApplication1.GCommon.ErrorMessages;

namespace WebApplication1.Data.Seeding.Utilities
{
    public class IdentitySeeder
    {
        public static string[] DefaultRoles = new string[] { "User", "Admin" };
        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (string roleName in DefaultRoles)
            {
                bool roleExists = roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult();
                if (roleExists)
                {
                    continue;
                }
                IdentityRole identityRole = new IdentityRole(roleName);
                IdentityResult result = roleManager.CreateAsync(identityRole).GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(string.Format(RoleCreateFail, roleName));
                }
            }
        }
        public static void SeedAdminUser(IServiceProvider serviceProvider, AdminUserSeedRequestModel requestModel)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            ApplicationUser? adminUser = userManager.FindByNameAsync(requestModel.UserName).GetAwaiter().GetResult();
            if (adminUser != null)
            {
                return;
            }
            adminUser = new ApplicationUser()
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                UserName = requestModel.UserName,
                Email = requestModel.Email,
                TownId = requestModel.TownId,
                PhoneNumber = requestModel.PhoneNumber
            };
            IdentityResult result = userManager.CreateAsync(adminUser, requestModel.Password).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Format(AdminUserSeedFail, requestModel.UserName));
            }
            result = userManager.AddToRoleAsync(adminUser, DefaultRoles[1]).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Format(AdminUserRoleAddFail, requestModel.UserName, DefaultRoles[1]));
            }
        }
    }
}
