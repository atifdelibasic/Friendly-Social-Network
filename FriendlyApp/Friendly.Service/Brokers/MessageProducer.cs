
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Friendly.Service.Brokers
{
    public class MessageProducer : IMessageProducer
    {
        public void SendingMessage<T>(T message)
        {
         
            try
            {
                Console.WriteLine("sending message");
                var factory = new ConnectionFactory()
                {
                    HostName = "rabbitmq",
                    UserName = "myuser",
                    Password = "mypass",
                    Port = 5672
                };

                var connection = factory.CreateConnection();


             var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "booking", durable: false, exclusive: false, autoDelete: false, arguments: null);

            //var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes("testiranje");

            channel.BasicPublish(exchange: "", routingKey: "booking", body: body, basicProperties: null);
                Console.WriteLine("message successfully published");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred while sending message: " + ex.Message);

            }

        }
    }
}
