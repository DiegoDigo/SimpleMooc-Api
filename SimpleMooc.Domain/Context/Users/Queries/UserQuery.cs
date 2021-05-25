using System;
using System.Linq.Expressions;
using SimpleMooc.Domain.Context.Users.Entities;
using SimpleMooc.Shared.Util;

namespace SimpleMooc.Domain.Context.Users.Queries
{
    public static class UserQuery
    {
        public static Expression<Func<User, bool>> FindByEmail(string email)
        {
            return user => user.Email.Equals(EmailUtil.EmailNormalize(email));
        }

        public static Expression<Func<User, bool>> FindById(Guid id)
        {
            return user => user.Id.Equals(id);
        }
        
    }
}