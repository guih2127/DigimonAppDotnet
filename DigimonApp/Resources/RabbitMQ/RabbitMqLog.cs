using DigimonApp.Domain.Models;

namespace DigimonApp.Resources.RabbitMQ
{
    public class RabbitMqLogMessage
    {
        public Digimon Message { get; set; }
        public bool Success { get; set; }
        public RabbitMqLogOperationType OperationType { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
