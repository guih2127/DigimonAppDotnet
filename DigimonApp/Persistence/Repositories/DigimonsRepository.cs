using DigimonApp.Domain.Models;
using DigimonApp.Domain.Repositories;
using DigimonApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DigimonApp.Persistence.Repositories
{
    public class DigimonsRepository : BaseRepository, IDigimonsRepository
    {
        public DigimonsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Digimon digimon)
        {
            await _context.Digimons.AddAsync(digimon);
        }

        public async Task<Digimon> FindByIdAsync(int id)
        {
            return await _context.Digimons.FindAsync(id);
        }

        public async Task<IEnumerable<Digimon>> ListAsync()
        {
            return await _context.Digimons.ToListAsync();
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
