using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Friendly.Database
{
    public class Notification : SoftDelete
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Message { get; set; }

        public bool IsRead { get; set; }

        [ForeignKey("Recipient")]
        public int RecipientId { get; set; }

        public User Recipient { get; set; }

        [ForeignKey("Sender")]
        public int SenderId { get; set; }

        public User Sender { get; set; }
    }
}
