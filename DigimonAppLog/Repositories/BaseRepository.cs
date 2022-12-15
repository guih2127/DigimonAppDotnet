using MongoDB.Driver;

namespace DigimonAppLog.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public IMongoDatabase GetMongoInstance()
        {
            var connectionString = "mongodb://127.0.0.1:27017";
            var dbName = "DigimonDbLog";

            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(dbName);

            return db;
        }

        public IMongoCollection<T> GetCollection<T>(string collection)
        {
            return GetMongoInstance().GetCollection<T>(collection);
        }

        public async Task CreateDocument<T>(string collection, T document)
        {
            await GetCollection<T>(collection).InsertOneAsync(document);
        }
    }
}
