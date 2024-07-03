
namespace Friendly.Model
{
    public class Message
    {
        public string Content { get; set; }
        public User Recipient { get; set; }
        public int RecipientId { get; set; }
        public User Sender { get; set; }
        public int SenderId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
