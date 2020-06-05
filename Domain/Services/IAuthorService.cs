using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Domain.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> ListAsync();
        Task<AuthorResponse> SaveAsync(Author author);
        Task<AuthorResponse> UpdateAsync(int id, Author author);
        Task<AuthorResponse> DeleteAsync(int id);
    }
}