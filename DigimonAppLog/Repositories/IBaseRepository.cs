using DigimonAppLog.Models;

namespace DigimonAppLog.Repositories
{
    public interface IBaseRepository
    {
        Task CreateDocument<T>(string collection, T document);
    }
}
