using CareerMate.Infrastructure.Persistence.Repositories.SysAdmins;
using CareerMate.Models;
using CareerMate.Models.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Seeds
{
    public class SysAdminSeed
    {
        private readonly IServiceProvider _serviceProvider;

        public SysAdminSeed(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        public async Task SeedUser()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sysAdminRepository = scope.ServiceProvider.GetRequiredService<SysAdminRepository>();
                var userManagerService = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                ApplicationUser newUser = new ApplicationUser()
                {
                    Email = "sysadmin@careermate.com",
                    UserName = "sysadmin@careermate.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = "First",
                    LastName = "User",
                };

                var createdUserResult = await userManagerService.CreateAsync(newUser, "Test@123");

                if (!createdUserResult.Succeeded)
                {
                    throw new Exception(createdUserResult.Errors.FirstOrDefault().Description);
                }

                await userManagerService.AddToRoleAsync(newUser, Roles.SysAdmin);
            }
        }
    }
}
