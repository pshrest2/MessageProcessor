using System.Threading.Tasks;

namespace RSMessageProcessor.RabbitMQ.Interface
{
    public interface IRabbitHandler<T> where T : class
    {
        Task HandleAsync(T message);
    }
}