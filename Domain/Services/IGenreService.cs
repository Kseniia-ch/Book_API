using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Domain.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> ListAsync();
        Task<GenreResponse> SaveAsync(Genre genre);
        Task<GenreResponse> UpdateAsync(int id, Genre genre);
        Task<GenreResponse> DeleteAsync(int id);
    }
}