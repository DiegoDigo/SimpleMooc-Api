using System;
using System.Linq.Expressions;
using SimpleMooc.Domain.Context.Users.Entities;

namespace SimpleMooc.Domain.Context.Users.Queries
{
    public static class RefreshTokenQuery
    {
        public static Expression<Func<RefreshToken, bool>> FindByToken(string token)
            => refreshToken => refreshToken.Token.Equals(token);

        public static Expression<Func<RefreshToken, bool>> FindByUserId(Guid userId)
            => refreshToken => refreshToken.User.Id.Equals(userId);
    }
}