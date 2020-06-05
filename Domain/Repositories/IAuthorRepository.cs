using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;

namespace BookAPI.Domain.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> ListAsync();
        Task AddAsync(Author author);
        void Update(Author author);
        Task<Author> FindByIdAsync(int id);
        Task<Author> FindByKeyFieldsAsync(string lastName, string firstName, string patronymic);
        void Remove(Author author);
    }
}