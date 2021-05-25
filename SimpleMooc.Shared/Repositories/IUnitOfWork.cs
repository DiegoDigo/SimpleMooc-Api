using System.Threading.Tasks;

namespace SimpleMooc.Shared.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}