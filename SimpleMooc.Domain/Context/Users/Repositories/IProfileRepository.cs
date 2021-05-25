using System;
using System.Threading.Tasks;
using SimpleMooc.Domain.Context.Users.Entities;

namespace SimpleMooc.Domain.Context.Users.Repositories
{
    public interface IProfileRepository
    {
        Task Save(Profile profile);
        void Update(Profile profile);
        Task<Profile> GetById(Guid id);
        Task<Profile> GetByUserId(Guid userId);
    }
}