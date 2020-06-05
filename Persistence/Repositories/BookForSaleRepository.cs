using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Persistence.Repositories
{
    public class BookForSaleRepository: BaseRepository, IBookForSaleRepository
    {
        public BookForSaleRepository(BookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BookForSale>> ListAsync()
        {
            return await context
                            .BooksForSale
                                .Include(x => x.BookOrdered)
                                .Include(x => x.Book)
                                    .ToListAsync();
        }

        public async Task<BookForSale> FindByIdAsync(int id)
        {
            return await context
                            .BooksForSale
                                .Include(x => x.BookOrdered)
                                .Include(x => x.Book)
                                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(BookForSale bookForSale)
        {
            await context.BooksForSale.AddAsync(bookForSale);
        }

        public void Remove(BookForSale bookForSale)
        {
            context.BooksForSale.Remove(bookForSale);
        }

        public void Update(BookForSale bookForSale)
        {
            context.BooksForSale.Update(bookForSale);
        }
    }
}