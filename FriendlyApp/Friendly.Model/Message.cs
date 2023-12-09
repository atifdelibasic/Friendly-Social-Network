
namespace Friendly.Model
{
    public class Message
    {
        public string Content { get; set; }
        public User Recipient { get; set; }
        public User Sender { get; set; } 
    }
}
