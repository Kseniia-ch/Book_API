using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Persistence.Context;

namespace BookAPI.Persistence.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(BookDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Role role)
        {
            await context.Roles.AddAsync(role);
        }

        public async Task<Role> FindByIdAsync(int id)
        {
            return await context.Roles.FindAsync(id);
        }

        public async Task<IEnumerable<Role>> ListAsync()
        {
            return await context.Roles.ToListAsync();
        }

        public void Remove(Role role)
        {
            context.Roles.Remove(role);
        }

        public void Update(Role role)
        {
            context.Roles.Update(role);
        }
    }
}