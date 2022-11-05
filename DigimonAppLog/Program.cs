using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using DigimonAppLog.Repositories;
using DigimonAppLog.Models;
using System.Text.Json;
using DigimonApp.Domain.Models;
using MongoDB.Bson;

class Receive
{
    public static void Main()
    {
        var logRepository = new LogRepository();
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "DigimonQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Digimon digimon = JsonSerializer.Deserialize<Digimon>(message);
                var digimonLog = new DigimonLog
                {
                    _id = ObjectId.GenerateNewId(),
                    Digimon = digimon,
                    OperationType = OperationType.SAVE,
                    Success = true
                };
                await logRepository.CreateLogDocument<DigimonLog>(digimonLog);

                Console.WriteLine(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "DigimonQueue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}