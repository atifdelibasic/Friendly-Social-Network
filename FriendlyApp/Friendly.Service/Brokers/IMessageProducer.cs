
namespace Friendly.Service.Brokers
{
    public interface IMessageProducer
    {
        public void SendingMessage<T>(T message);
    }
}
