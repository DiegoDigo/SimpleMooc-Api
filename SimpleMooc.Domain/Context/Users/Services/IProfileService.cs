using System;
using System.Threading.Tasks;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Context.Users.Services
{
    public interface IProfileService
    {
        Task<BaseResponse> GetByUserId(Guid id);
    }
}