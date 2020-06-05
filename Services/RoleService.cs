using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Domain.Services;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUnitOfWork unitOfWork;
        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            this.roleRepository = roleRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<RoleResponse> DeleteAsync(int id)
        {
            var existingRole = await roleRepository.FindByIdAsync(id);
            if (existingRole == null)
                return new RoleResponse("Role not found");
            try
            {
                roleRepository.Remove(existingRole);
                await unitOfWork.CompleteAsync();

                return new RoleResponse(existingRole);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Error when deleting role: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Role>> ListAsync()
        {
            return await roleRepository.ListAsync();
        }

        public async Task<RoleResponse> SaveAsync(Role role)
        {
            try
            {
                await roleRepository.AddAsync(role);
                await unitOfWork.CompleteAsync();

                return new RoleResponse(role);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Error when saving the role: {ex.Message}");
            }
        }

        public async Task<RoleResponse> UpdateAsync(int id, Role role)
        {
            var existingRole = await roleRepository.FindByIdAsync(id);
            if (existingRole == null)
                return new RoleResponse("Role not found");
            
             existingRole.Name = role.Name;

            try
            {
                roleRepository.Update(existingRole);
                await unitOfWork.CompleteAsync();

                return new RoleResponse(existingRole);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Error when updating role: {ex.Message}");
            }
        }

     
    }
}