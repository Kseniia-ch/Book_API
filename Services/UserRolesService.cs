using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Domain.Services;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Services
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        public UserRolesService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> DeleteRole(int userId, int roleId)
        {
            try
            {
                var user = await userRepository.FindByIdAsync(userId);
                user.UserRoles.Remove(user.UserRoles.FirstOrDefault(x => x.RoleId == roleId));
                await unitOfWork.CompleteAsync();
                user = await userRepository.FindByIdAsync(userId);
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when deleting role: {ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> ListUsersByRoleAsync(int roleId)
        {
            var users = await userRepository.ListAsync();
            var usersInRole = users.Where(x => x.UserRoles.Contains(x.UserRoles.SingleOrDefault(y => y.RoleId == roleId)));
            return usersInRole;

        }

        public async Task<UserResponse> SetRole(int userId, int roleId)
        {
            try
            {
                var user = await userRepository.FindByIdAsync(userId);
                user.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
                await unitOfWork.CompleteAsync();
                user = await userRepository.FindByIdAsync(userId);
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when setting role: {ex.Message}");
            }

        }
    }
}