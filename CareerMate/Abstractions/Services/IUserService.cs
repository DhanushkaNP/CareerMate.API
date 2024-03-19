using CareerMate.Models;
using System;
using System.Threading.Tasks;

namespace CareerMate.Abstractions.Services
{
    public interface IUserService
    {
        Task<Guid> CreateUser(string email, string password, string role);

        Task<String> Login(string email, string password);
    }
}
