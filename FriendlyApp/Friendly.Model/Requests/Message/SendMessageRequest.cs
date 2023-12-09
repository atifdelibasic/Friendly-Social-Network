namespace Friendly.Model.Requests.Message
{
    public class SendMessageRequest
    {
        public string Content { get; set; }
        public int RecipientId { get; set; }
    }
}
