using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Persistence.Repositories
{
    public class BookOrderedRepository: BaseRepository, IBookOrderedRepository
    {
        public BookOrderedRepository(BookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BookOrdered>> ListAsync()
        {
            return await context
                            .BooksOrdered
                                .Include(x => x.User)
                                .Include(x => x.BookForSale)
                                    .ThenInclude(x => x.Book)
                                        .ToListAsync();
        }

        public async Task<BookOrdered> FindByIdAsync(int id)
        {
            return await context
                            .BooksOrdered
                                .Include(x => x.User)
                                .Include(x => x.BookForSale)
                                    .ThenInclude(x => x.Book)
                                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(BookOrdered bookOrdered)
        {
            await context.BooksOrdered.AddAsync(bookOrdered);
        }

        public void Remove(BookOrdered bookOrdered)
        {
            context.BooksOrdered.Remove(bookOrdered);
        }

        public void Update(BookOrdered bookOrdered)
        {
            context.BooksOrdered.Update(bookOrdered);
        }
    }
}