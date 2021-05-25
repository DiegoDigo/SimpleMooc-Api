using System.Threading.Tasks;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Courses.Services
{
    public interface ICourseService
    {
        Task<BaseResponse> Search(string search);
        Task<BaseResponse> GetAll();
    }
}