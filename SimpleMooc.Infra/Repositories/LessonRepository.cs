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
    public class LessonRepository : ILessonRepository
    {
        private readonly SimpleMoocDataContext _context;

        public LessonRepository(SimpleMoocDataContext context)
        {
            _context = context;
        }

        public async Task Save(Lesson lesson)
            => await _context.AddAsync(lesson);


        public async Task<IEnumerable<Lesson>> GetAllByCourse(Guid courseId)
            => await _context.Lessons.AsNoTracking()
                .Where(LessonQuery.FindLessonByCourse(courseId))
                .ToListAsync();

        public async Task<Lesson> GetById(Guid id)
            => await _context.Lessons.SingleOrDefaultAsync(LessonQuery.FindById(id));

        public async Task<Lesson> GetByIdAndCourseId(Guid lessonId, Guid courseId)
            => await _context.Lessons.SingleOrDefaultAsync(LessonQuery.FindByIdAndCourseId(lessonId, courseId));
    }
}