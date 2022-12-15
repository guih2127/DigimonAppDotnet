using DigimonAppLog.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class LogConsumer
{
    public static void Main()
    {
        var logService = new LogService();
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
                await logService.CreateLogDocument(message);

                Console.WriteLine(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "DigimonQueue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine("Log Application is running...");
            Console.ReadLine();
        }
    }
}