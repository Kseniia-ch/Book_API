using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Domain.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> ListAsync();
        Task<RoleResponse> SaveAsync(Role role);
        Task<RoleResponse> UpdateAsync(int id, Role role);
        Task<RoleResponse> DeleteAsync(int id);
    }
}