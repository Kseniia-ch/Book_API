using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Persistence.Context;

namespace BookAPI.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(BookDbContext context) : base(context)
        {
        }

        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await context
                            .Users
                                .Include(x => x.BooksForSale)
                                .Include(x => x.UserRoles)
                                    .ThenInclude(x => x.Role)
                                        .FirstOrDefaultAsync(x => x.Id == id);                  
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await context
                            .Users
                                .Include(x => x.BooksForSale)
                                .Include(x => x.UserRoles)
                                    .ThenInclude(x => x.Role)
                                        .ToListAsync();
        }

        public void Remove(User user)
        {
            context.Users.Remove(user);
        }

        public void Update(User user)
        {
            context.Users.Update(user);
        }
    }
}