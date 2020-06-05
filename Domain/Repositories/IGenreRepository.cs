using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;

namespace BookAPI.Domain.Repositories
{
    public interface IGenreRepository
    {   
         Task<IEnumerable<Genre>> ListAsync();
         Task AddAsync(Genre genre);
         void Update(Genre genre);
         Task<Genre> FindByIdAsync(int id);
         Task<Genre> FindByKeyFieldsAsync(string name);
         void Remove(Genre genre);
    }
}