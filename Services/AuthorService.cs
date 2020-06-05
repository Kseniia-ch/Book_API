using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Domain.Services;
using BookAPI.Domain.Services.Communication;
using BookAPI.Persistence.Repositories;

namespace BookAPI.Services
{
    public class AuthorService: IAuthorService
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IUnitOfWork unitOfWork;
        public AuthorService(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            this.authorRepository = authorRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<AuthorResponse> DeleteAsync(int id)
        {
            var existingAuthor = await authorRepository.FindByIdAsync(id);
            if (existingAuthor == null)
                return new AuthorResponse("Author not found");
            if (existingAuthor.Books.Count > 0)
                return new AuthorResponse("Author has books");
            try
            {
                authorRepository.Remove(existingAuthor);
                await unitOfWork.CompleteAsync();

                return new AuthorResponse(existingAuthor);
            }
            catch (Exception ex)
            {
                return new AuthorResponse($"Error when deleting Author: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Author>> ListAsync()
        {
            return await authorRepository.ListAsync();
        }

        public async Task<AuthorResponse> SaveAsync(Author Author)
        {
            try
            {
                await authorRepository.AddAsync(Author);
                await unitOfWork.CompleteAsync();

                return new AuthorResponse(Author);
            }
            catch (Exception ex)
            {
                return new AuthorResponse($"Error when saving the Author: {ex.Message}");
            }
        }

        public async Task<AuthorResponse> UpdateAsync(int id, Author Author)
        {
            var existingAuthor = await authorRepository.FindByIdAsync(id);
            if (existingAuthor == null)
                return new AuthorResponse("Author not found");
            
            existingAuthor.FirstName = Author.FirstName;
            existingAuthor.LastName = Author.LastName;
            existingAuthor.Patronymic = Author.Patronymic;

            try
            {
                authorRepository.Update(existingAuthor);
                await unitOfWork.CompleteAsync();

                return new AuthorResponse(existingAuthor);
            }
            catch (Exception ex)
            {
                return new AuthorResponse($"Error when updating Author: {ex.Message}");
            }
        }
    }
}