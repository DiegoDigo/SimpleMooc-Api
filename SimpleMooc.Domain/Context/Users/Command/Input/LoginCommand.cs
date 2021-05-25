using MediatR;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Users.Command.Input
{
    public record LoginCommand(string Email, string Password) : IRequest<BaseResponse>;

}