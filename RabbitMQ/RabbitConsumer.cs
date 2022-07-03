using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RSMessageProcessor.RabbitMQ.Dtos;
using RSMessageProcessor.RabbitMQ.Interface;

namespace RSMessageProcessor.RabbitMQ
{
    public class RabbitConsumer<T> : IRabbitConsumer<T> where T : class
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly RabbitConfig _config;
        private IRabbitHandler<T> _handler;
        private IConnection _connection;
        private IModel _channel;

        public RabbitConsumer(IOptions<RabbitConfig> configOptions, IServiceScopeFactory serviceScopeFactory)
        {
            _config = configOptions.Value;
            _serviceScopeFactory = serviceScopeFactory;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _handler = scope.ServiceProvider.GetRequiredService<IRabbitHandler<T>>();
            }

            var factory = _config.Uri == null
                ? new ConnectionFactory
                {
                    HostName = _config.HostName,
                    UserName = _config.UserName,
                    Password = _config.Password,
                    Port = _config.Port,
                    VirtualHost = _config.VHost,
                }
                : new ConnectionFactory
                {
                    Uri = new Uri(_config.Uri)
                };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task ConsumeMessage(string queue, CancellationToken cancellationToken)
        {
            _channel.QueueDeclare(queue, true, false, false, null);

            cancellationToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var content = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<T>(content);

                await _handler.HandleAsync(message);
                _channel.BasicAck(eventArgs.DeliveryTag, true);
            };
            _channel.BasicConsume(queue, autoAck: false, consumer);
            return Task.CompletedTask;
        }
    }
}

