namespace DigimonAppLog.Services
{
    public interface ILogService
    {
        Task CreateLogDocument(string message);
    }
}
