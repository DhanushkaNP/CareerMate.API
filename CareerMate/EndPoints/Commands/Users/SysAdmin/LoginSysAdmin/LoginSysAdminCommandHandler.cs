using CareerMate.EndPoints.Handlers;
using CareerMate.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.SysAdmin.LoginSysAdmin
{
    public class LoginSysAdminCommandHandler : IRequestHandler<LoginSysAdminCommand, BaseResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUserRoles> _roleManager;
        private readonly IConfiguration _configuration;

        public LoginSysAdminCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUserRoles> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<BaseResponse> Handle(LoginSysAdminCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user == null)
            {
                return new UnauthorizedResponse("Wrong email address");
            }

            bool isCorrectPassword = await _userManager.CheckPasswordAsync(user, command.Password);

            if (!isCorrectPassword)
            {
                return new UnauthorizedResponse("Wrong email address");
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("JWTID", Guid.NewGuid().ToString()),
            };

            foreach (var role in userRoles) 
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = GenerateNewJsonWebToken(authClaims);

            return new LoginSysAdminCommandResponse()
            {
                Token = token
            };
        }

        private string GenerateNewJsonWebToken(List<Claim> authClaims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTAuthenticatom:Secret"]));

            var tokenObject = new JwtSecurityToken(
                issuer: _configuration["JWTAuthenticatom:ValidIssuer"],
                audience: _configuration["JWTAuthenticatom:ValidAudience"],
                expires: DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }
    }
}
