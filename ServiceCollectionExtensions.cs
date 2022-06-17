using RSMessageProcessor.Kafka;
using RSMessageProcessor.Kafka.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using RSMessageProcessor.Dtos;

namespace RSMessageProcessor
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMessageProcessor(this IServiceCollection services, KafkaConfigurationDto config)
        {
            if (!services.Any(descriptor => descriptor.ServiceType.Assembly == Assembly.GetExecutingAssembly()))
            {
                services.AddSingleton(config.ProducerConfig);
                services.AddSingleton(config.ConsumerConfig);

                services.AddSingleton(typeof(IKafkaConsumer<,>), typeof(KafkaConsumer<,>));
                services.AddSingleton(typeof(IKafkaProducer<,>), typeof(KafkaProducer<,>));
            }

            return services;
        }
    }
}