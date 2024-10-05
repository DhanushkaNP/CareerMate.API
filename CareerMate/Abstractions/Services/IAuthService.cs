using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CareerMate.Abstractions.Services
{
    public interface IAuthService
    {
        Task<string> GenerateNewJsonWebToken(string id,string email, IList<string> userRoles);
    }
}
