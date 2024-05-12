using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

ConnectionFactory factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
factory.UserName = "myuser";
factory.Password = "mypass";

try
{

    // Add retry logic to wait for RabbitMQ to become available
    const int maxRetries = 10;
    int retryCount = 0;

    IConnection _conn = null;
    IModel channel = null;

    while (retryCount < maxRetries)
    {
        try
        {
            _conn = factory.CreateConnection();
            channel = _conn.CreateModel();
            Console.WriteLine("Connection established sucessfully!");
            break; // Break out of the loop if connection is successful
        }
        catch (Exception ex)
        {
            // Log or print the exception if needed
            Console.WriteLine($"Failed to connect to RabbitMQ. Retry count: {retryCount + 1}. Error: {ex.Message}");

            retryCount++;
            Thread.Sleep(10000); // Wait for 5 seconds before retrying
        }
    }

    if(channel != null)
    {
        Console.WriteLine("channel is different than null");
        channel.QueueDeclare(queue: "hello", durable: false, exclusive: false,autoDelete: false, arguments: null);
    }


    var consumer = new EventingBasicConsumer(channel);

consumer.Received += async (model, eventArgs) =>
{

    Console.WriteLine("message recieved");
    byte[] body = eventArgs.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);

    Console.WriteLine("dosla poruka");
    Console.WriteLine(message);

    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    ReportModel deserializedReport = JsonSerializer.Deserialize<ReportModel>(message, options);
    Console.WriteLine("deserialized object");
    Console.WriteLine(deserializedReport.ToString());
    Console.WriteLine(deserializedReport.additionalComment);



    using (var httpClient = new HttpClient())
    {
        try
        {
            Console.WriteLine("sibni request");
            var response = await Task.Run(async () =>
            {
                return await httpClient.PostAsync("http://web_api:80/user/register", new StringContent("", Encoding.UTF8, "application/json"));
            });
            
            Console.WriteLine("evo ga response");
            Console.WriteLine(response.StatusCode);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Error making HTTP request: " + ex.Message);
            Console.WriteLine( ex.StackTrace);

        }
    }

    Console.WriteLine("dosla poruka jarooooo " + "");
};

channel.BasicConsume(queue:"hello",autoAck: true,consumer: consumer);

}
catch (Exception ex)
{
    Console.WriteLine("Exception occurred: " + ex.Message);
    Console.WriteLine(ex.InnerException);
    Console.Write(ex.StackTrace);
    Console.WriteLine(factory.HostName);
    Console.WriteLine(factory.UserName);
    Console.WriteLine(factory.Password);

}

Thread.Sleep(Timeout.Infinite);

public class ReportModel
{
    public int? postId { get; set; }
    public int? commentId { get; set; }
    public int reportReasonId { get; set; }
    public string additionalComment { get; set; }
}