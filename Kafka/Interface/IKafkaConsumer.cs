using System.Threading;
using System.Threading.Tasks;

namespace MessageProcessor.Kafka.Interface
{
    public interface IKafkaConsumer<TKey, TValue>
    {
        Task Consume(string topic, CancellationToken stoppingToken);
    }
}
