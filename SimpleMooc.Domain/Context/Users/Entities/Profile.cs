using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Users.Entities
{
    public class Profile : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string UrlImage { get; private set; }
        public User User { get; private set; }

        public Profile()
        {
        }

        public Profile(string firstName, string lastName, User user)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = $"{firstName} {lastName}";
            User = user;
        }

        public void ChangeUrlImage(string url)
        {
            UrlImage = url;
        }

        public void ChangeFirstName(string firstName)
        {
            FirstName = firstName;
        }

        public void ChangeLastName(string lastName)
        {
            LastName = lastName;
        }

        public void ChangeFullName()
        {
            FullName = $"{FirstName} {LastName}";
        }
        
    }
}