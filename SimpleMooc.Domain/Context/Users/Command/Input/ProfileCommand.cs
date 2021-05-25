using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Users.Command.Input
{
    public record ProfileCommand(
        string FirstName,
        string LastName,
        IFormFile Image
    ) : IRequest<BaseResponse>
    {
        public Guid UserId { get; set; }
    }
}