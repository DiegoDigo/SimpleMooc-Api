using System.Threading.Tasks;
using SimpleMooc.Infra.DataContext;
using SimpleMooc.Shared.Repositories;

namespace SimpleMooc.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SimpleMoocDataContext _context;

        public UnitOfWork(SimpleMoocDataContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}