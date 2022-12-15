using DigimonApp.Domain.Models;
using DigimonApp.Resources.RabbitMQ;
using DigimonAppLog.Models;
using DigimonAppLog.Repositories;
using MongoDB.Bson;
using System.Text.Json;

namespace DigimonAppLog.Services
{
    public class LogService : ILogService
    {
        private readonly BaseRepository _baseRepository;
        private readonly string _collection;

        public LogService()
        {
            _baseRepository = new BaseRepository();
            _collection = "Logs";        
        }

        public async Task CreateLogDocument(string messageJson)
        {
            RabbitMqLogMessage message = JsonSerializer.Deserialize<RabbitMqLogMessage>(messageJson);
            var log = new DigimonLog
            {
                _id = ObjectId.GenerateNewId(),
                Digimon = message.Message,
                OperationType = message.OperationType,
                Success = message.Success
            };

            await _baseRepository.CreateDocument(_collection, log);
        }
    }
}
