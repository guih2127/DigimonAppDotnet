using MongoDB.Driver;

namespace DigimonAppLog.Repositories
{
    public class LogRepository : ILogRepository
    {
        public IMongoDatabase GetMongoInstance()
        {
            var connectionString = "mongodb://127.0.0.1:27017";
            var dbName = "DigimonDbLog";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(dbName);

            return db;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return GetMongoInstance().GetCollection<T>("Logs");
        }

        public async Task CreateLogDocument<T>(T document)
        {
            await GetCollection<T>().InsertOneAsync(document);
        }
    }
}
