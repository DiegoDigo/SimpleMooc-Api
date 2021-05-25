using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Users.Command.Input
{
    public record ProfileUpdateCommand(
        Guid Id,
        string FirstName,
        string LastName,
        IFormFile Image,
        string Email
    ) : IRequest<BaseResponse>
    {
        public Guid UserId { get; set; }
    }
}