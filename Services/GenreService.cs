using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Domain.Services;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;
        private readonly IUnitOfWork unitOfWork;
        public GenreService(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
        {
            this.genreRepository = genreRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<GenreResponse> DeleteAsync(int id)
        {
            var existingGenre = await genreRepository.FindByIdAsync(id);
            if (existingGenre == null)
                return new GenreResponse("Genre not found");
            if (existingGenre.Books.Count > 0)
                return new GenreResponse("Genre is used in books");
            try
            {
                genreRepository.Remove(existingGenre);
                await unitOfWork.CompleteAsync();

                return new GenreResponse(existingGenre);
            }
            catch (Exception ex)
            {
                return new GenreResponse($"Error when deleting Genre: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Genre>> ListAsync()
        {
            return await genreRepository.ListAsync();
        }

        public async Task<GenreResponse> SaveAsync(Genre genre)
        {
            try
            {
                await genreRepository.AddAsync(genre);
                await unitOfWork.CompleteAsync();

                return new GenreResponse(genre);
            }
            catch (Exception ex)
            {
                return new GenreResponse($"Error when saving the Genre: {ex.Message}");
            }
        }

        public async Task<GenreResponse> UpdateAsync(int id, Genre genre)
        {
            var existingGenre = await genreRepository.FindByIdAsync(id);
            if (existingGenre == null)
                return new GenreResponse("Genre not found");
            
            existingGenre.Name = genre.Name;

            try
            {
                genreRepository.Update(existingGenre);
                await unitOfWork.CompleteAsync();

                return new GenreResponse(existingGenre);
            }
            catch (Exception ex)
            {
                return new GenreResponse($"Error when updating Genre: {ex.Message}");
            }
        }
    }
}