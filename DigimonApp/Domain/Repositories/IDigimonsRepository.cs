using DigimonApp.Domain.Models;

namespace DigimonApp.Domain.Repositories
{
    public interface IDigimonsRepository
    {
        Task<IEnumerable<Digimon>> ListAsync();
        Task AddAsync(Digimon digimon);
        Task<Digimon> FindByIdAsync(int id);
        void Update(Digimon digimon);
        void Remove(Digimon digimon);
    }
}
