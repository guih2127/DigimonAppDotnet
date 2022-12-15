using DigimonApp.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace DigimonAppLog.Models
{
    public class DigimonLog : Log
    {

        [BsonElement("Digimon")]
        public Digimon Digimon { get; set; }
    }
}
