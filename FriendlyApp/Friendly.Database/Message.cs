using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Friendly.Database
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        [ForeignKey("SenderId")]
        public int SenderId { get; set; }

        public User Sender { get; set; }

        [ForeignKey("SenderId")]
        public int RecieverId { get; set; }

        public User Reciever { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
