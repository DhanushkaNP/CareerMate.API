using CareerMate.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CareerMate
{
    public static class RolePolicyConfigurations
    {
        public static IServiceCollection AddRolesPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AllowedSysAdmin",
                    policy => policy.RequireRole(Roles.SysAdmin));
            });

            return services;
        }
    }
}
