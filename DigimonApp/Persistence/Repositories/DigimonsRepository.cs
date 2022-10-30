using DigimonApp.Domain.Models;
using DigimonApp.Domain.Repositories;
using DigimonApp.Persistence.Contexts;
using DigimonApp.Resources;
using Microsoft.EntityFrameworkCore;

namespace DigimonApp.Persistence.Repositories
{
    public class DigimonsRepository : BaseRepository, IDigimonsRepository
    {
        public DigimonsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Digimon>> ListAsync(ListDigimonResource resource)
        {
            var lastId = (resource.Page - 1) * resource.NumberOfItens;
            var numberOfItens = resource.NumberOfItens != 0 ? resource.NumberOfItens : int.MaxValue;

            return await _context.Digimons
                        .OrderBy(d => d.Id)
                        .Where(d => d.Id > lastId)
                        .Take(numberOfItens)
                        .ToListAsync();
        }

        public async Task AddAsync(Digimon digimon)
        {
            await _context.Digimons.AddAsync(digimon);
        }

        public async Task<Digimon> FindByIdAsync(int id)
        {
            return await _context.Digimons.FindAsync(id);
        }

        public void Update(Digimon digimon)
        {
            _context.Digimons.Update(digimon);
        }

        public void Remove(Digimon category)
        {
            _context.Digimons.Remove(category);
        }
    }
}
