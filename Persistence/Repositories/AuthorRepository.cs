using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Persistence.Repositories
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(BookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Author>> ListAsync()
        {
            return await context.Authors.ToListAsync();
        }

        public async Task<Author> FindByIdAsync(int id)
        {
            return await context
                            .Authors                                
                                .Include(x => x.Books)
                                    .ThenInclude(x => x.Book)
                                        .FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<Author> FindByKeyFieldsAsync(string lastName, string firstName, string patronymic)
        {
            return await context
                            .Authors                                
                                .Include(x => x.Books)
                                    .ThenInclude(x => x.Book)
                                        .FirstOrDefaultAsync(x => x.LastName == lastName && x.FirstName == firstName && x.Patronymic == patronymic);
        }

        public async Task AddAsync(Author author)
        {
            await context.Authors.AddAsync(author);
        }

        public void Remove(Author author)
        {
            context.Authors.Remove(author);
        }

        public void Update(Author author)
        {
            context.Authors.Update(author);
        }

        
    }
}