using DigimonApp.Resources.RabbitMQ;

namespace DigimonApp.Domain.Services
{
    public interface IRabbitMQService
    {
        public void SendLogMessage(RabbitMqLogMessage log, string queue);
    }
}
