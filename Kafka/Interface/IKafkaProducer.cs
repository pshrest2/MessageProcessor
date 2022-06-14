using System.Threading.Tasks;

namespace MessageProcessor.Kafka.Interface
{
    public interface IKafkaProducer<TKey, TValue>
    {
        Task ProduceAsync(string topic, TKey key, TValue value);
    }
}