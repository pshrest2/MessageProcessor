using System.Threading;
using System.Threading.Tasks;

namespace RSMessageProcessor.RabbitMQ.Interface
{
    public interface IRabbitConsumer<T>
    {
        Task ConsumeMessage(string queue, CancellationToken cancellationToken);
    }
}