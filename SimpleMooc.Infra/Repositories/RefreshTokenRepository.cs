using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleMooc.Domain.Context.Users.Entities;
using SimpleMooc.Domain.Context.Users.Queries;
using SimpleMooc.Domain.Context.Users.Repositories;
using SimpleMooc.Infra.DataContext;

namespace SimpleMooc.Infra.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly SimpleMoocDataContext _context;

        public RefreshTokenRepository(SimpleMoocDataContext context)
        {
            _context = context;
        }

        public void Update(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
        }

        public async Task Save(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
        }

        public async Task<RefreshToken> GetByToken(string token)
            => await _context.RefreshTokens
                .Include(x => x.User)
                .SingleOrDefaultAsync(RefreshTokenQuery.FindByToken(token));


        public async Task<RefreshToken> GetByUserId(Guid userId)
            => await _context.RefreshTokens
                .Include(x => x.User)
                .SingleOrDefaultAsync(RefreshTokenQuery.FindByUserId(userId));
    }
}