using SimpleMooc.Domain.Context.Users.Entities;

namespace SimpleMooc.Domain.Context.Users.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }
}