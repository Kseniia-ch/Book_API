using System.Threading.Tasks;
using BookAPI.Domain.Models;

namespace BookAPI.Domain.Services
{
    public interface IAuthService
    {
        Task<User> Authenticate(string login, string password);
    }
}