using BookAPI.Persistence.Context;

namespace BookAPI.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly BookDbContext context;
        public BaseRepository(BookDbContext context)
        {
            this.context = context;
        }
    }
}