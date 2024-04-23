using CareerMate.Abstractions;
using CareerMate.Abstractions.Exceptions;
using CareerMate.Abstractions.Services;
using CareerMate.Models.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUserRoles> _roleManager;
        private readonly IAuthService _authService;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationUserRoles> roleManager,
            IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public async Task<Guid> CreateUser(string email, string password, string role, string firstName, string lastName, CancellationToken cancellation)
        {
            var isExistingUser = await _userManager.FindByEmailAsync(email);

            if (isExistingUser != null)
            {
                throw new BadRequestException(ErrorCodes.ExisitingUser ,"Existing user");
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                Email = email,
                UserName = email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = firstName,
                LastName = lastName,
            };

            var createdUserResult = await _userManager.CreateAsync(newUser, password);

            if (!createdUserResult.Succeeded)
            {
                throw new BadRequestException(createdUserResult.Errors.FirstOrDefault().Description);
            }

            await _userManager.AddToRoleAsync(newUser, role);

            return newUser.Id;
        }

        public async Task<LoginUserDetailModel> Login(string email, string password, List<string> rolesLookingFor, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new BadRequestException(ErrorCodes.LoggingUserDetailsIncorrect, "Wrong user details");
            }

            bool isCorrectPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!isCorrectPassword)
            {
                throw new BadRequestException(ErrorCodes.LoggingUserDetailsIncorrect ,"Wrong user details");
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Count != 0 && !rolesLookingFor.All(role => userRoles.Contains(role)))
            {
                throw new BadRequestException(ErrorCodes.LoggingUserDetailsIncorrect, "Wrong user details");
            }

            string token = await _authService.GenerateNewJsonWebToken(user.Id.ToString(), user.Email, userRoles);

            return new LoginUserDetailModel(token, user.Id);
        }

        public async Task DeleteAsync(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());

            await _userManager.DeleteAsync(user);
        }

        public async Task<ApplicationUser> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new NotFoundException<ApplicationUser>();
            }

            return user;
        }

        public async Task UpdatePassword(Guid id,  string password, CancellationToken cancellationToken)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(id.ToString());

            var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);

            var result = _userManager.ResetPasswordAsync(applicationUser, token, password);

            if (!result.IsCompletedSuccessfully)
            {
                throw new BadRequestException("Something happen when updating password");
            }
        }
    }
}
