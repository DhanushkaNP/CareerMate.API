using CareerMate.Abstractions.Exceptions;
using CareerMate.Abstractions.Services;
using CareerMate.Models;
using CareerMate.Models.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerMate.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUserRoles> _roleManager;
        private readonly IAuthService _authService;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUserRoles> roleManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<Guid> CreateUser(string email, string password, string role)
        {
            var isExistingUser = await _userManager.FindByEmailAsync(email);

            if (isExistingUser != null)
            {
                throw new BadRequestException("Existing user");
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                Email = email,
                UserName = email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = null,
                LastName = null,
            };

            var createdUserResult = await _userManager.CreateAsync(newUser, password);

            if (!createdUserResult.Succeeded)
            {
                throw new BadRequestException(createdUserResult.Errors.FirstOrDefault().Description);
            }

            await _userManager.AddToRoleAsync(newUser, Roles.SysAdmin);

            return newUser.Id;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new UnauthorizedException("Wrong email address");
            }

            bool isCorrectPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!isCorrectPassword)
            {
                throw new UnauthorizedException("Wrong email address");
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            string token = await _authService.GenerateNewJsonWebToken(user.Id.ToString(), user.Email, userRoles);

            return token;
        }
    }
}
