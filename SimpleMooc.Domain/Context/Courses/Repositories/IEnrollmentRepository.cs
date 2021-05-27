using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Domain.Context.Courses.Repositories
{
    public interface IEnrollmentRepository
    {
        Task Save(Enrollment enrollment);
        Task<Enrollment> GetByUserIdAndCourseId(Guid userId, Guid courseId);
        Task<IEnumerable<Enrollment>> GetByUserId(Guid userId);
    }
}