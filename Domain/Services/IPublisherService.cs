using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Domain.Services
{
    public interface IPublisherService
    {
        Task<IEnumerable<Publisher>> ListAsync();
        Task<PublisherResponse> SaveAsync(Publisher publisher);
        Task<PublisherResponse> UpdateAsync(int id, Publisher publisher);
        Task<PublisherResponse> DeleteAsync(int id);
    }
}