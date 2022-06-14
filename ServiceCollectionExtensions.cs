using MessageProcessor.Kafka;
using MessageProcessor.Kafka.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace MessageProcessor
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMessageProcessor(this IServiceCollection services)
        {
            if (!services.Any(descriptor => descriptor.ServiceType.Assembly == Assembly.GetExecutingAssembly()))
            {
                services.AddTransient(typeof(IKafkaConsumer<,>), typeof(KafkaConsumer<,>));
                services.AddTransient(typeof(IKafkaProducer<,>), typeof(KafkaProducer<,>));
            }

            return services;
        }
    }
}