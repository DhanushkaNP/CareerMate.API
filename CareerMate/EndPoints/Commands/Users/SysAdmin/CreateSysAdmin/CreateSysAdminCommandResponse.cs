using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using System;

namespace CareerMate.EndPoints.Commands.Users.SysAdmin.CreateSysAdmin
{
    public class CreateSysAdminCommandResponse : BaseResponse
    {
        public CreateSysAdminCommandResponse() : base(StatusCodes.Status201Created)
        {
        }

        public String Id { get; set; }
    }
}
