using DigimonApp.Domain.Repositories;
using DigimonApp.Domain.Services;
using DigimonApp.Persistence.Contexts;
using DigimonApp.Persistence.Repositories;
using DigimonApp.Services;

namespace DigimonApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IDigimonsRepository, DigimonsRepository>();
        }

        public static void RegisterServices(this IServiceCollection collection)
        {
            collection.AddScoped<IUnitOfWork, UnitOfWork>();
            collection.AddScoped<IDigimonsService, DigimonsService>();
        }
    }
}
