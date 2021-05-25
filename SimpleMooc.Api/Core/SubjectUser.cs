using System;
using System.Linq;
using System.Security.Claims;

namespace SimpleMooc.Api.Core
{
    public class SubjectUser
    {
        public static Guid GetId(ClaimsPrincipal user)
        {
            var id = user?.Claims?.FirstOrDefault(c => c.Type.Equals("id"))?.Value;
            return new Guid(id ?? string.Empty);
        }
    }
}