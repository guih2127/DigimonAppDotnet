using DigimonApp.Domain.Models;
using DigimonApp.Domain.Services.Communication;
using DigimonApp.Resources;

namespace DigimonApp.Domain.Services
{
    public interface IUsersService
    {
        Task<UserResponse> SaveAsync(User user, string password);
        Task<LoginResponse> LoginAsync(LoginUserResource user);
        Task<string> GenerateToken(User user);
    }
}
