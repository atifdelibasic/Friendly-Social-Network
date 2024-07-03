using System;
using System.Text;
using RabbitMQ.Client;

namespace publisher_api.Services
{
    public interface IMessageService
    {
        bool Enqueue(string message);
        void UserRegisterMessage(string message);
    }

    public class MessageService : IMessageService
    {
        ConnectionFactory _factory;
        IConnection _conn;
        IModel _channel;
        public MessageService()
        {
            try
            {
                var hostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "rabbitmq";
                var port = Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? "5672";
                var userName = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "myuser";
                var password = Environment.GetEnvironmentVariable("RABBITMQ_PASS") ?? "mypass";

                _factory = new ConnectionFactory() { HostName = hostName, Port = int.Parse(port) };
                _factory.UserName = userName;
                _factory.Password = password;
                _conn = _factory.CreateConnection();
                _channel = _conn.CreateModel();
                _channel.QueueDeclare(queue: "reportQueue",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                _channel.QueueDeclare(queue: "userRegisterQueue",
                                       durable: false,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null);
            }
            catch (Exception)
            {

            }

        }

        public void UserRegisterMessage(string messageString)
        {
            var body = Encoding.UTF8.GetBytes(messageString);
            _channel.BasicPublish(exchange: "",
                                routingKey: "userRegisterQueue",
                                basicProperties: null,
                                body: body);
            Console.WriteLine(" [x] Published {0} to RabbitMQ", messageString);
        }

        public bool Enqueue(string messageString)
        {
            var body = Encoding.UTF8.GetBytes(messageString);
            _channel.BasicPublish(exchange: "",
                                routingKey: "reportQueue",
                                basicProperties: null,
                                body: body);
            Console.WriteLine(" [x] Published {0} to RabbitMQ", messageString);
            return true;
        }
    }
}
