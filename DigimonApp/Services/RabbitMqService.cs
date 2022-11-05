using DigimonApp.Domain.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DigimonApp.Services
{
    public static class RabbitMqService
    {
        public static void SendBasicMessage(Digimon digimon, string queue)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(digimon));

                channel.BasicPublish(exchange: "",
                                     routingKey: queue,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
