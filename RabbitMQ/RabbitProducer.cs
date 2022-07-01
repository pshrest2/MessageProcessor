using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RSMessageProcessor.RabbitMQ.Dtos;
using RSMessageProcessor.RabbitMQ.Interface;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace RSMessageProcessor.RabbitMQ
{
    public class RabbitProducer<T> : IRabbitProducer<T> where T : class
    {
        private readonly RabbitConfig _config;
        private IConnection _connection;
        private IModel _channel;
        public RabbitProducer(IOptions<RabbitConfig> configOptions)
        {
            _config = configOptions.Value;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = _config.HostName,
                UserName = _config.UserName,
                Password = _config.Password,
                Port = _config.Port,
                VirtualHost = _config.VHost,
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task ProduceMessage(string queue, T message)
        {
            _channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "", queue, basicProperties: null, body);
            return Task.CompletedTask;
        }
    }
}

