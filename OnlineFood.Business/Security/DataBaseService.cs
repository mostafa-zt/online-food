using Microsoft.AspNetCore.Identity;
using OnlineFood.Business.Contracts;
using OnlineFood.Domain.Entities;
using OnlineFood.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Business.Security
{
    public static class DataBaseService
    {
        public static async Task SeedDataBaseAsync(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationDbContext dbContext)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, dbContext);
        }

        private static async Task SeedUsers(UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            var userName = "onlinefood";
            var password = "123456";
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new User()
                {
                    Firstname = "mostafa",
                    Lastname = "zartaj",
                    UserName = userName,
                    IsSystemAccount = true,
                    Email = "mostafa.zartaj@gmail.com",
                    CreatorDateTime = DateTime.Now
                };

                IdentityResult resultUser = await userManager.CreateAsync(user, password);
                var result1 = await userManager.SetLockoutEnabledAsync(user, false);

                if (resultUser.Succeeded)
                {
                    //var userRoles = userManager.GetRolesAsync(user).Result;
                    //if (userRoles.Any(a => a == StandardRoles.Administrator)) return;
                    var roleAdded = await userManager.AddToRoleAsync(user, StandardRoles.Administrator);
                    //var result2 = userManager.AddClaimAsync(user, new Claim(StandardClaims.FullName, user.Fullname)).Result;
                    var usernameClaimAdded = await userManager.AddClaimAsync(user, new Claim(StandardClaims.Username, user.UserName));
                    var rolenameClaimed = await userManager.AddClaimAsync(user, new Claim(StandardClaims.RoleName, StandardRoles.GetSysmteRoles().FirstOrDefault(w => w.Name == StandardRoles.Administrator).Description));

                    dbContext.Set<Restaurant>().Add(new Restaurant() { CreatorDateTime = DateTime.Now, CreatorUserId = user.Id });
                    dbContext.Set<Seller>().Add(new Seller() { CreatorDateTime = DateTime.Now, CreatorUserId = user.Id, User = user });
                    await dbContext.SaveAllChangesAsync(updateCommonFields: false);
                }
            }

            var userRoles = await userManager.GetRolesAsync(user);
            if (userRoles.Any(a => a == StandardRoles.Administrator)) return;
            var addToRoleResult = await userManager.AddToRoleAsync(user, StandardRoles.Administrator);
        }

        private static async Task SeedRoles(RoleManager<Role> roleManager)
        {
            var standardRoles = StandardRoles.GetSysmteRoles();

            foreach (var role in from roleName in standardRoles
                                 let role = roleManager.FindByNameAsync(roleName.Name).Result
                                 where role == null
                                 select new Role
                                 {
                                     Name = roleName.Name,
                                     Description = roleName.Description,
                                     IsSystemRole = true,
                                     IsBanned = false,
                                     CreatorDateTime = DateTime.Now
                                 })
            {
                IdentityResult roleResult = await roleManager.CreateAsync(role);
            }
        }
    }
}
