using System;
using System.Threading.Tasks;
using SimpleMooc.Domain.Context.Users.Entities;

namespace SimpleMooc.Domain.Context.Users.Repositories
{
    public interface IUserRepository
    {
        Task Save(User user);
        void Update(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetById(Guid id);
    }
}