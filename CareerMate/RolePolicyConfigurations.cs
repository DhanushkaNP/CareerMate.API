using CareerMate.Abstractions.Enums;
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
                options.AddPolicy(Policies.SysAdminOnly,
                    policy => policy.RequireRole(Roles.SysAdmin));
                options.AddPolicy(Policies.CoordinatorLevel,
                    policy => policy.RequireRole(Roles.SysAdmin ,Roles.Coordinator));
                options.AddPolicy(Policies.CoordinatorAssistantLevel,
                    policy => policy.RequireRole(Roles.SysAdmin , Roles.Coordinator, Roles.CoordinatorAssistant));
                options.AddPolicy(Policies.CoordinatorOnly,
                    policy => policy.RequireRole(Roles.Coordinator));
            });

            return services;
        }
    }
}
