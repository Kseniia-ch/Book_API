using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Domain.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> ListAsync();
        Task<BookResponse> SaveAsync(Book book);
        Task<BookResponse> UpdateAsync(int id, Book book);
        Task<BookResponse> DeleteAsync(int id);
    }
}