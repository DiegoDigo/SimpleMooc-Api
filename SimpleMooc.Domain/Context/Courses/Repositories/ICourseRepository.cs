using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleMooc.Domain.Context.Courses.Entities;

namespace SimpleMooc.Domain.Context.Courses.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> SearchCourse(string search);
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetById(Guid id);
        Task Save(Course course);
        void Update(Course course);
    }
}