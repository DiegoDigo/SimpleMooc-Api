using System;

namespace SimpleMooc.Domain.Context.Users.Command.Output
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlImage { get; set; }
        public string Email { get; set; }

        public ProfileResponse()
        {
        }
    }
}