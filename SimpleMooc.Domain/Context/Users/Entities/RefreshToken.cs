using System;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Users.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; private set; }
        public DateTime Expires { get; private set; }
        public User User { get; private set; }

        public RefreshToken()
        {
        }

        public RefreshToken(string token, User user)
        {
            Token = token;
            Expires = DateTime.UtcNow.AddDays(7);
            User = user;
        }


        public bool IsExpired() => DateTime.UtcNow >= Expires;

        public void ChangeRefreshToken(string token)
        {
            Token = token;
        }
        
    }
}