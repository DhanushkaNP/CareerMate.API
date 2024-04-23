using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Abstractions.Services
{
    public interface IUserService
    {
        Task<Guid> CreateUser(string email, string password, string role, string firstName, string lastName, CancellationToken cancellationToken);

        Task<LoginUserDetailModel> Login(string email, string password, List<string> rolesLookingFor, CancellationToken cancellationToken);

        Task DeleteAsync(Guid Id);

        Task<ApplicationUser> GetUserById(Guid id, CancellationToken cancellationToken);

        Task UpdatePassword(Guid id, string password, CancellationToken cancellationToken);
    }
}
