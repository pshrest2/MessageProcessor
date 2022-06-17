using RSMessageProcessor.Kafka;
using RSMessageProcessor.Kafka.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Confluent.Kafka;

namespace RSMessageProcessor
{
    public static class ServiceCollectionExtension
    {
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
                services.AddSingleton(typeof(IKafkaConsumer<,>), typeof(KafkaConsumer<,>));
            }

            return services;
        }
    }
}