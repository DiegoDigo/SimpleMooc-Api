using System;
using System.Linq.Expressions;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Domain.Context.Courses.Queries
{
    public static class EnrollmentQuery
    {
        public static Expression<Func<Enrollment, bool>> FindByUserIdAndCourseId(Guid userId, Guid courseId)
        {
            return enrollment => enrollment.Course.Id.Equals(courseId)
                                 && enrollment.User.Id.Equals(userId);
        }

        public static Expression<Func<Enrollment, bool>> FindByUserId(Guid userId)
        {
            return enrollment => enrollment.User.Id.Equals(userId);
        }
    }
}