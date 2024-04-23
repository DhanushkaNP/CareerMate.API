using CareerMate.Abstractions.Services;
using CareerMate.Infrastructure.Persistence.Seeds;
using CareerMate.Infrastructure.Persistence;
using CareerMate.Models.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using CareerMate.Services.AuthServices;
using CareerMate.Services.UserServices;
using Microsoft.Extensions.Logging;

namespace CareerMate
{
    public static class ServicesConfigurations
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DB
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"));

                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });

            // Add Identity
            services
                .AddIdentity<ApplicationUser, ApplicationUserRoles>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Config Identity
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
            });

            // Add Authentication and JwtBearer
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = configuration["JWTAuthenticatom:ValidIssuer"],
                        ValidAudience = configuration["JWTAuthenticatom:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTAuthenticatom:Secret"]))
                    };
                });

            // Add Mediator 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));


            // Add Automapper
            services.AddAutoMapper(
                options =>
                {
                    options.AllowNullCollections = true;
                },
                Assembly.GetAssembly(typeof(Program)));

            services.AddLogging(logging => logging.AddConsole());

            return services;
        }

        public static IServiceCollection RegisterSystemServices(this IServiceCollection services)
        {
            services.AddScoped<IdentityRoleSeed>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

        //public static IServiceCollection AddRepositories(this IServiceCollection services)
        //{
        //    //services.AddSingleton<IUnitOfWork, AppDbContext>();
        //    services.AddScoped<ISysAdminRepository,SysAdminRepository>();

        //    return services;
        //}
    }
}
