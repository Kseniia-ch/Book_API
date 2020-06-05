using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;

namespace BookAPI.Domain.Repositories
{
    public interface IPublisherRepository
    {
         Task<IEnumerable<Publisher>> ListAsync();
         Task AddAsync(Publisher publisher);
         void Update(Publisher publisher);
         Task<Publisher> FindByIdAsync(int id);
         Task<Publisher> FindByKeyFieldsAsync(string name, string city);
         void Remove(Publisher publisher);
    }
}