using RSMessageProcessor.Kafka;
using RSMessageProcessor.Kafka.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Confluent.Kafka;
using RSMessageProcessor.RabbitMQ.Dtos;
using RSMessageProcessor.RabbitMQ.Interface;
using RSMessageProcessor.RabbitMQ;
using Microsoft.Extensions.Configuration;


namespace RSMessageProcessor
{
    public static class ServiceCollectionExtension
    {
        // Kafka
        public static IServiceCollection AddKafkaProducer(this IServiceCollection services, ProducerConfig config)
        {
            if (!services.Any(descriptor => descriptor.ServiceType.Assembly == Assembly.GetExecutingAssembly()))
            {
                services.AddSingleton(config);
                services.AddSingleton(typeof(IKafkaProducer<,>), typeof(KafkaProducer<,>));
            }

            return services;
        }
        public static IServiceCollection AddKafkaConsumer(this IServiceCollection services, ConsumerConfig config)
        {
            if (!services.Any(descriptor => descriptor.ServiceType.Assembly == Assembly.GetExecutingAssembly()))
            {
                services.AddSingleton(config);
                services.AddSingleton(typeof(IRabbitConsumer<,>), typeof(KafkaConsumer<,>));
            }

            return services;
        }

        // RabbitMQ
        public static IServiceCollection AddRabbitProducer(this IServiceCollection services, IConfigurationSection rabbitConfig)
        {
            if (!services.Any(descriptor => descriptor.ServiceType.Assembly == Assembly.GetExecutingAssembly()))
            {
                services.Configure<RabbitConfig>(rabbitConfig);
                services.AddSingleton(typeof(IRabbitProducer<>), typeof(RabbitProducer<>));
            }

            return services;
        }
        public static IServiceCollection AddRabbitConsumer(this IServiceCollection services, IConfigurationSection rabbitConfig)
        {
            if (!services.Any(descriptor => descriptor.ServiceType.Assembly == Assembly.GetExecutingAssembly()))
            {
                services.Configure<RabbitConfig>(rabbitConfig);
                services.AddSingleton(typeof(IRabbitConsumer<>), typeof(RabbitConsumer<>));
            }

            return services;
        }
    }
}