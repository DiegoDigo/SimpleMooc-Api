using System;
using System.Threading.Tasks;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Courses.Services
{
    public interface IEnrollmentService
    {
        Task<BaseResponse> GetAll(Guid userId);
        Task<BaseResponse> CloseEnrollmentByCourseIdAndUserId(Guid courseId, Guid userId);
    }
}