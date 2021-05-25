using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleMooc.Domain.Context.Users.Entities;
using SimpleMooc.Domain.Context.Users.Queries;
using SimpleMooc.Domain.Context.Users.Repositories;
using SimpleMooc.Infra.DataContext;

namespace SimpleMooc.Infra.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly SimpleMoocDataContext _context;

        public ProfileRepository(SimpleMoocDataContext context)
        {
            _context = context;
        }

        public async Task Save(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
        }

        public void Update(Profile profile)
        {
            _context.Profiles.Update(profile);
        }

        public Task<Profile> GetByUserId(Guid userId)
            => _context.Profiles.SingleOrDefaultAsync(ProfileQuery.FindByUserId(userId));

        public Task<Profile> GetById(Guid userId)
            => _context.Profiles.Include(profile => profile.User).SingleOrDefaultAsync(ProfileQuery.FindById(userId));
    }
}