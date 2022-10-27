using DigimonApp.Domain.Models;
using DigimonApp.Domain.Services.Communication;

namespace DigimonApp.Domain.Services
{
    public interface IDigimonsService
    {
        Task<IEnumerable<Digimon>> ListAsync();
        Task<DigimonResponse> SaveAsync(Digimon digimon);
        Task<DigimonResponse> UpdateAsync(int id, Digimon digimon);
        Task<DigimonResponse> DeleteAsync(int id);
    }
}
