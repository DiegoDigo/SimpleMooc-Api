using MediatR;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Users.Command.Input
{
    public record RefreshTokenCommand(string Refresh) : IRequest<BaseResponse>;
}