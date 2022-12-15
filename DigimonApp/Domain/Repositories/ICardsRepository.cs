using DigimonApp.Domain.Models;

namespace DigimonApp.Domain.Repositories
{
    public interface ICardsRepository
    {
        Task AddListAsync(IEnumerable<Card> cards);
    }
}
