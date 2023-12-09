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

        [ForeignKey("RecipientId")]
        public int RecipientId { get; set; }

        public User Recipient { get; set; }
    }
}
