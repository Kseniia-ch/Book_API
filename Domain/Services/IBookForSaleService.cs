using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Domain.Services
{
    public interface IBookForSaleService
    {
            Task<IEnumerable<BookForSale>> ListAsync();
            Task<BookForSaleResponse> SaveAsync(BookForSale bookForSale);
            Task<BookForSaleResponse> UpdateAsync(int id, BookForSale bookForSale);
            Task<BookForSaleResponse> DeleteAsync(int id);
        }
}