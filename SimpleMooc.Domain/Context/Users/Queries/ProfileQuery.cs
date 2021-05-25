using System;
using System.Linq.Expressions;
using SimpleMooc.Domain.Context.Users.Entities;

namespace SimpleMooc.Domain.Context.Users.Queries
{
    public static class ProfileQuery
    {
        public static Expression<Func<Profile, bool>> FindByUserId(Guid userId) 
        => profile => profile.User.Id.Equals(userId);
        
        
        public static Expression<Func<Profile, bool>> FindById(Guid id)
        => profile => profile.Id.Equals(id);
        
    }
}