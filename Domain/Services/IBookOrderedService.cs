using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Domain.Services
{
    public interface IBookOrderedService
    {
        Task<IEnumerable<BookOrdered>> ListAsync();
        Task<BookOrderedResponse> SaveAsync(BookOrdered bookOrdered);
        Task<BookOrderedResponse> UpdateAsync(int id, BookOrdered bookOrdered);
        Task<BookOrderedResponse> DeleteAsync(int id);
    }
}