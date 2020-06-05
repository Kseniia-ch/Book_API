using System.Threading.Tasks;
using BookAPI.Domain.Repositories;
using BookAPI.Persistence.Context;

namespace BookAPI.Persistence.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly BookDbContext context;
        public UnitOfWork(BookDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}