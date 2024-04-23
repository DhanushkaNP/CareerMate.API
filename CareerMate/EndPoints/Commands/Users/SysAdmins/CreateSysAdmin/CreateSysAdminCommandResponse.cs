using CareerMate.EndPoints.Handlers;
using CareerMate.Models.Entities.SysAdmins;
using Microsoft.AspNetCore.Http;
using System;

namespace CareerMate.EndPoints.Commands.Users.SysAdmins.CreateSysAdmin
{
    public class CreateSysAdminCommandResponse : BaseResponse
    {
        public CreateSysAdminCommandResponse() : base(StatusCodes.Status201Created)
        {
        }

        public Guid Id { get; set; }
    }
}
