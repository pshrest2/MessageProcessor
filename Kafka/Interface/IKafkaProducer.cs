using System.Threading.Tasks;

namespace RSMessageProcessor.Kafka.Interface
{
    public interface IKafkaProducer<TKey, TValue>
    {
        Task ProduceAsync(string topic, TKey key, TValue value);
        void Dispose();
    }
}