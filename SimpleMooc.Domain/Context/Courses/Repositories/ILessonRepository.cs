using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Domain.Context.Courses.Repositories
{
    public interface ILessonRepository
    {
        Task Save(Lesson lesson);
        Task<IEnumerable<Lesson>> GetAllByCourse(Guid courseId);
        Task<Lesson> GetById(Guid id);
        Task<Lesson> GetByIdAndCourseId(Guid lessonId, Guid courseId);
    }
}