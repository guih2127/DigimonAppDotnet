using DigimonApp.Domain.Models;
using DigimonApp.Domain.Repositories;
using DigimonApp.Persistence.Contexts;

namespace DigimonApp.Persistence.Repositories
{
    public class CardsRepository : BaseRepository, ICardsRepository
    {
        public CardsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddListAsync(IEnumerable<Card> cards)
        {
            await _context.Cards.AddRangeAsync(cards);
        }
    }
}
