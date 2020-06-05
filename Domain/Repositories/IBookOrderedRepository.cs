using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;

namespace BookAPI.Domain.Repositories
{
    public interface IBookOrderedRepository
    {
        Task<IEnumerable<BookOrdered>> ListAsync();
        Task AddAsync(BookOrdered bookOrdered);
        void Update(BookOrdered bookOrdered);
        Task<BookOrdered> FindByIdAsync(int id);
        void Remove(BookOrdered bookOrdered);
    }
}