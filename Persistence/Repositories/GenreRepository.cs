using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Persistence.Repositories
{
    public class GenreRepository : BaseRepository, IGenreRepository
    {
        public GenreRepository(BookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Genre>> ListAsync()
        {
            return await context
                            .Genres
                                .Include(x => x.Books)
                                    .ToListAsync();
        }

        public async Task<Genre> FindByIdAsync(int id)
        {
            return await context
                            .Genres
                                .Include(x => x.Books)
                                    .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Genre> FindByKeyFieldsAsync(string name)
        {
            return await context
                            .Genres
                                .Include(x => x.Books)
                                    .FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task AddAsync(Genre genre)
        {
            await context.Genres.AddAsync(genre);
        }

        public void Remove(Genre genre)
        {
            context.Genres.Remove(genre);
        }

        public void Update(Genre genre)
        {
            context.Genres.Update(genre);
        }

       
    }
}