using DigimonAppLog.Models;

namespace DigimonAppLog.Repositories
{
    public interface ILogRepository
    {
        Task CreateLogDocument<T>(T document);
    }
}
