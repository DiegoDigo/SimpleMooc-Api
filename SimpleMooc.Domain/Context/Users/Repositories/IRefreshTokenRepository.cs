using System;
using System.Threading.Tasks;
using SimpleMooc.Domain.Context.Users.Entities;

namespace SimpleMooc.Domain.Context.Users.Repositories
{
    public interface IRefreshTokenRepository
    {
        void Update(RefreshToken refreshToken);
        Task Save(RefreshToken refreshToken);
        Task<RefreshToken> GetByToken(string token);
        Task<RefreshToken> GetByUserId(Guid userId);
        
    }
}