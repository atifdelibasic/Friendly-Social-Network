namespace Friendly.Model.Requests.Message
{
    public class CreateMessageRequest
    {
        public string Content { get; set; }
        public int RecipientId { get; set; }
        public int SenderId { get; set; }
    }
}
