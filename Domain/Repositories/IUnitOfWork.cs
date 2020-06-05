using System.Threading.Tasks;

namespace BookAPI.Domain.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}