using System.Threading.Tasks;

namespace MessageProcessor.Kafka.Interface
{
    public interface IKafkaHandler<TKey, TValue>
    {
        Task HandleAsync(TKey key, TValue value);
    }
}
