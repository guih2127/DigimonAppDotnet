using DigimonApp.Domain.Models;
using DigimonApp.Resources.RabbitMQ;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DigimonAppLog.Models
{
    public class Log
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("OperationType")]
        public RabbitMqLogOperationType OperationType { get; set; }

        [BsonElement("Success")]
        public bool Success { get; set; }

        [BsonElement("ErrorMessage")]
        public string? ErrorMessage { get; set; }
    }
}
