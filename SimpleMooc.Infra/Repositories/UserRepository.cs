using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleMooc.Domain.Context.Users.Entities;
using SimpleMooc.Domain.Context.Users.Queries;
using SimpleMooc.Domain.Context.Users.Repositories;
using SimpleMooc.Infra.DataContext;

namespace SimpleMooc.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SimpleMoocDataContext _context;

        public UserRepository(SimpleMoocDataContext context)
        {
            _context = context;
        }

        public async Task Save(User user)
            => await _context.Users.AddAsync(user);

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<User> GetByEmail(string email)
            => await _context.Users
                .SingleOrDefaultAsync(UserQuery.FindByEmail(email));

        public Task<User> GetById(Guid id)
            => _context.Users.SingleOrDefaultAsync(UserQuery.FindById(id));
    }
}