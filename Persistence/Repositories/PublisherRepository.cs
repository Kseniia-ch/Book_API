using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Persistence.Repositories
{
    public class PublisherRepository : BaseRepository, IPublisherRepository
    {
        public PublisherRepository(BookDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Publisher>> ListAsync()
        {
            return await context
                            .Publishers
                                .Include(x => x.Books)
                                    .ToListAsync();
        }

        public async Task<Publisher> FindByIdAsync(int id)
        {
            return await context
                            .Publishers
                                .Include(x => x.Books)
                                    .FirstOrDefaultAsync(x => x.Id == id);
                                        
        }
 
        public async Task<Publisher> FindByKeyFieldsAsync(string name, string city)
        {
            return await context
                            .Publishers
                                .Include(x => x.Books)
                                    .FirstOrDefaultAsync(x => x.Name == name && x.City == city);
        }

        public async Task AddAsync(Publisher publisher)
        {
            await context.Publishers.AddAsync(publisher);
        }

        public void Remove(Publisher publisher)
        {
            context.Publishers.Remove(publisher);
        }

        public void Update(Publisher publisher)
        {
            context.Publishers.Update(publisher);
        }

       
    }
}