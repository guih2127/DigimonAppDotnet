using DigimonApp.Domain.Models;
using DigimonApp.Resources.RabbitMQ;

namespace DigimonApp.Utils
{
    public static class LogUtils
    {
        public static RabbitMqLogMessage CreateLogObject(
            Digimon message, bool success, RabbitMqLogOperationType operationType, string? errorMessage = null)
        {
            return new RabbitMqLogMessage
            {
                Message = message,
                Success = success,
                ErrorMessage = errorMessage,
                OperationType = operationType
            };
        }
    }
}
