using DigimonApp.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace DigimonApp.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task AddAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
    }
}
