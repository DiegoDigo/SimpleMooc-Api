using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleMooc.Domain.Context.Courses.Entities;
using SimpleMooc.Domain.Context.Courses.Queries;
using SimpleMooc.Domain.Context.Courses.Repositories;
using SimpleMooc.Infra.DataContext;

namespace SimpleMooc.Infra.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly SimpleMoocDataContext _context;

        public EnrollmentRepository(SimpleMoocDataContext context)
        {
            _context = context;
        }

        public async Task Save(Enrollment enrollment)
        {
            await _context.AddAsync(enrollment);
        }

        public Task<Enrollment> GetByUserIdAndCourseId(Guid userId, Guid courseId)
            => _context.Enrollments.SingleOrDefaultAsync(EnrollmentQuery.FindByUserIdAndCourseId(userId, courseId));
    }
}