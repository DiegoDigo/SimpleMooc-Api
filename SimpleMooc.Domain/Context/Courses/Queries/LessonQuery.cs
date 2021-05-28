using System;
using System.Linq.Expressions;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Domain.Context.Courses.Queries
{
    public static class LessonQuery
    {
        public static Expression<Func<Lesson, bool>> FindLessonByCourse(Guid courseId)
        {
            return lesson => lesson.Course.Id.Equals(courseId);
        }

        public static Expression<Func<Lesson, bool>> FindById(Guid id)
        {
            return lesson => lesson.Id.Equals(id);
        }

        public static Expression<Func<Lesson, bool>> FindByIdAndCourseId(Guid lessonId, Guid courseId)
        {
            return lesson => lesson.Id.Equals(lessonId) && lesson.Course.Id.Equals(courseId);
        }
    }
}