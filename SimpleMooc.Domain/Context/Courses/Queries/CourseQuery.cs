using System;
using System.Linq.Expressions;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Domain.Context.Courses.Queries
{
    public static class CourseQuery
    {
        public static Expression<Func<Course, bool>> SearchInNameOrDescription(string query)
        {
            return course => course.Name.Contains(query) || course.Description.Contains(query);
        }
        
        public static Expression<Func<Course, bool>> SearchSlug(string slug)
        {
            return course => course.Slug.Equals(slug);
        }
        
        public static Expression<Func<Course, bool>> FindById(Guid id)
        {
            return course => course.Id.Equals(id);
        }
    }
}