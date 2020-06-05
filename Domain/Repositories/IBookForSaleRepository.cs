using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;

namespace BookAPI.Domain.Repositories
{
    public interface IBookForSaleRepository
    {
        Task<IEnumerable<BookForSale>> ListAsync();
        Task AddAsync(BookForSale bookForSale);
        void Update(BookForSale bookForSale);
        Task<BookForSale> FindByIdAsync(int id);
        void Remove(BookForSale bookForSale);
    }
}