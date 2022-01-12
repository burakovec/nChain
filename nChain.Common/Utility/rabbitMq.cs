using nChain.Common.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace nChain.Common.Utility
{
    public class rabbitMq
    {
        private static ConnectionFactory connectionFactory;
        private static IConnection connection;
        private static IModel channel;
        public rabbitMq()
        {
            connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
        }

        public Task PublishMessage(int nonce, string hash)
        {
            channel.QueueDeclare(queue: "Mine", durable: false, exclusive: false, autoDelete: true, arguments: null);
            Request request = new Request
            {
                Nonce = nonce,
                Hash = hash
            };

            string message = JsonConvert.SerializeObject(request);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "Mine", basicProperties: null, body: body);

            return Task.CompletedTask;
        }
    }
}
