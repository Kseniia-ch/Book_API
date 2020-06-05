using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Persistence.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(BookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> ListAsync()
        {
            return await context
                            .Books
                                .Include(x => x.Genre)
                                .Include(x => x.Publisher)
                                .Include(x => x.BooksForSale)
                                .Include(x => x.Authors)
                                    .ThenInclude(x => x.Author)
                                        .ToListAsync();
        }

        public async Task<Book> FindByIdAsync(int id)
        {
            return await context
                            .Books
                                .Include(x => x.Genre)
                                .Include(x => x.Publisher)
                                .Include(x => x.BooksForSale)
                                .Include(x => x.Authors)
                                    .ThenInclude(x => x.Author)
                                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Book book)
        {
            await context.Books.AddAsync(book);
        }

        public void Remove(Book book)
        {
            context.Books.Remove(book);
        }

        public void Update(Book book)
        {
            context.Books.Update(book);
        }
    }
}