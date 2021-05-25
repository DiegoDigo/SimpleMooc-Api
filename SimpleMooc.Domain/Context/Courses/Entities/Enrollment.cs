using SimpleMooc.Domain.Context.Courses.Entities.Enums;
using SimpleMooc.Domain.Context.Users.Entities;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Courses.Entities
{
    public class Enrollment : BaseEntity
    {
        public User User { get; private set; }
        public Course Course { get; private set; }
        public EStatus Status { get; private set; }

        public Enrollment()
        {
        }

        public Enrollment(User user, Course course)
        {
            User = user;
            Course = course;
            Status = EStatus.Pending;
        }
    }
}