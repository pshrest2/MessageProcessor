using System.Threading.Tasks;

namespace RSMessageProcessor.Kafka.Interface
{
    public interface IKafkaHandler<TKey, TValue>
    {
        Task HandleAsync(TKey key, TValue value);
    }
}
