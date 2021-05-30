using SimpleMooc.Domain.Context.Users.Entities.Enums;
using SimpleMooc.Shared.Entities;
using SimpleMooc.Shared.Util;

namespace SimpleMooc.Domain.Context.Users.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; }
        public ERole Role { get; private set; }

        public User(string email)
        {
            Email = EmailUtil.EmailNormalize(email);
            Role = ERole.Customer;
            Active = false;
        }

        public User()
        {
        }

        public void ChangePassword(string password)
        {
            Password = PasswordUtil.HashPassword(password);
        }


        public bool VerifyPassword(string password, string hashPassword)
            => PasswordUtil.Verify(password, hashPassword);


        public void ChangeActive(bool active)
        {
            Active = active;
        }

        public void ChangeEmail(string email)
        {
            Email = email;
        }

        public void ChangeRole(ERole role)
        {
            Role = role;
        }
    }
}