using DigimonApp.Domain.Services.Communication;

namespace DigimonApp.Domain.Services
{
    public interface ICardsService
    {
        Task<BaseResponse> ImportDigimonCardsFromDigimonTcgAPI();
    }
}
