using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Domain.Services;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await userRepository.FindByIdAsync(id);
            if (existingUser == null)
                return new UserResponse("User not found");
            if (existingUser.BooksForSale.Count > 0)
                return new UserResponse("User has books");
            if (existingUser.BooksOrdered.Count > 0)
                return new UserResponse("User ordered books");
            try
            {
                userRepository.Remove(existingUser);
                await unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when deleting user: {ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await userRepository.ListAsync();
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                await userRepository.AddAsync(user);
                await unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(int id, User user)
        {
            var existingUser = await userRepository.FindByIdAsync(id);
            if (existingUser == null)
                return new UserResponse("User not found");
            
            existingUser.Firstname = user.Firstname;
            existingUser.Lastname = user.Lastname;
            existingUser.Login = user.Login;
            existingUser.Password = user.Password;
            existingUser.Phone = user.Phone;

            try
            {
                userRepository.Update(existingUser);
                await unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Error when updating user: {ex.Message}");
            }
        }
    }
}