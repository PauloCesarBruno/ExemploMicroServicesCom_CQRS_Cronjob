using System.Threading.Tasks;

namespace Infrastructure.Publishers.Interfaces
{
    public interface IMessagePublisher<T> where T : class
    {
        Task PublishMessage(T message);
    }
}
