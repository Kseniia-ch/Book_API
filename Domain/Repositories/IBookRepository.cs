using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;

namespace BookAPI.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> ListAsync();
        Task AddAsync(Book book);
        void Update(Book book);
        Task<Book> FindByIdAsync(int id);
        void Remove(Book book);
    }
}