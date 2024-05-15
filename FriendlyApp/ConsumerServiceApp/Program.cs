using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

ConnectionFactory factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
factory.UserName = "myuser";
factory.Password = "mypass";

try
{

    const int maxRetries = 20;
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
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to RabbitMQ. Retry count: {retryCount + 1}. Error: {ex.Message}");

            retryCount++;
            Thread.Sleep(10000); 
        }
    }

    if(channel != null)
    {
        Console.WriteLine("channel is different than null");
        channel.QueueDeclare(queue: "reportQueue", durable: false, exclusive: false,autoDelete: false, arguments: null);
    }


    var consumer = new EventingBasicConsumer(channel);

consumer.Received += async (model, eventArgs) =>
{

    byte[] body = eventArgs.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);

    Console.WriteLine(message);

    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    ReportModel report = JsonSerializer.Deserialize<ReportModel>(message, options);
    Console.WriteLine("deserialized object");
    Console.WriteLine(report.additionalComment);

    using (var httpClient = new HttpClient())
    {
        string token = report.Token;
        if (token.StartsWith("Bearer "))
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Substring(7));
        }
        else
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
        try
        {
            string jsonReport = JsonSerializer.Serialize(report);
            var content = new StringContent(jsonReport, Encoding.UTF8, "application/json");
            Console.WriteLine("content");
            Console.WriteLine(content);

           
                Console.WriteLine("posalji request");
            var response =  await httpClient.PostAsync("http://web_api:80/report", content);

            Console.WriteLine("status code");
            Console.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response content: " + responseContent);
            }
            else
            {
                Console.WriteLine("Error: " + response.ReasonPhrase);
            }

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Error making HTTP request: " + ex.Message);
            Console.WriteLine( ex.StackTrace);

        }
    }

};

channel.BasicConsume(queue: "reportQueue", autoAck: true,consumer: consumer);

}
catch (Exception ex)
{
    Console.WriteLine("Exception occurred: " + ex.Message);
}


// keep it hanging 
Thread.Sleep(Timeout.Infinite);

public class ReportModel
{
    public int? postId { get; set; }
    public int? commentId { get; set; }
    public int reportReasonId { get; set; }
    public string additionalComment { get; set; }
    public string Token { get; set; }
}