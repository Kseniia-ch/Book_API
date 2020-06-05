using System.Threading.Tasks;
using BookAPI.Domain.Models;
using System.Collections.Generic;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Domain.Services
{
    public interface IUserRolesService
    {
        Task<IEnumerable<User>> ListUsersByRoleAsync(int roleId);
        Task<UserResponse> SetRole(int userId, int roleId);
        Task<UserResponse> DeleteRole(int userId, int roleId);
    }
}