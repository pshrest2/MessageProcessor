using System.Threading.Tasks;

namespace RSMessageProcessor.RabbitMQ.Interface
{
    public interface IRabbitProducer<T>
    {
        Task ProduceMessage(string queue, T message);
    }
}