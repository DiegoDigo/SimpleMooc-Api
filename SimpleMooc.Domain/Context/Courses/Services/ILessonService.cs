using System;
using System.Threading.Tasks;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Courses.Services
{
    public interface ILessonService
    {
        Task<BaseResponse> GetAllLessonByCourse(Guid courseId);
    }
}