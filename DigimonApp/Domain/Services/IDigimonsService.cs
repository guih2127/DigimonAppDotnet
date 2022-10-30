using DigimonApp.Domain.Models;
using DigimonApp.Domain.Services.Communication;
using DigimonApp.Resources;

namespace DigimonApp.Domain.Services
{
    public interface IDigimonsService
    {
        Task<IEnumerable<Digimon>> ListAsync(ListDigimonResource resource);
        Task<DigimonResponse> SaveAsync(Digimon digimon);
        Task<DigimonResponse> UpdateAsync(int id, Digimon digimon);
        Task<DigimonResponse> DeleteAsync(int id);
    }
}
