using CareerMate.Models;
using CareerMate.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Seeds
{
    public class IdentityRoleSeed
    {
        private readonly IServiceProvider _serviceProvider;

        public IdentityRoleSeed(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SeedRoles()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationUserRoles>>();
                List<string> roles = new List<string>() { Roles.SysAdmin, Roles.Coordinator, Roles.CoordinatorAssistant, Roles.Student, Roles.Company, Roles.CompanySupervisor };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new ApplicationUserRoles(role));
                    }
                }
            }
        }

    }
}
