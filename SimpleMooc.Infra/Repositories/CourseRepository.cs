using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleMooc.Domain.Context.Courses.Entities;
using SimpleMooc.Domain.Context.Courses.Queries;
using SimpleMooc.Domain.Context.Courses.Repositories;
using SimpleMooc.Infra.DataContext;

namespace SimpleMooc.Infra.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SimpleMoocDataContext _context;

        public CourseRepository(SimpleMoocDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> SearchCourse(string search)
            => await _context.Courses
                .AsNoTracking()
                .Where(CourseQuery.SearchInNameOrDescription(search))
                .ToListAsync();

        public async Task Save(Course course)
        {
            await _context.AddAsync(course);
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }

        public async Task<IEnumerable<Course>> GetAll()
            => await _context.Courses
                .AsNoTracking()
                .ToListAsync();

        public async Task<Course> GetById(Guid id)
            => await _context.Courses.SingleOrDefaultAsync(CourseQuery.FindById(id));

        public async Task<Course> GetBySlug(string slug)
            => await _context.Courses.SingleOrDefaultAsync(CourseQuery.SearchSlug(slug));
    }
}