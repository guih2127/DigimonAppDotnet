using DigimonApp.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DigimonAppLog.Models
{
    public class DigimonLog
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("OperationType")]
        public OperationType OperationType { get; set; }

        [BsonElement("Digimon")]
        public Digimon Digimon { get; set; }

        [BsonElement("Success")]
        public bool Success { get; set; }

        [BsonElement("ErrorMessage")]
        public string? ErrorMessage { get; set; }
    }
}
