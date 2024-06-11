using System;

namespace Friendly.Model
{
    public class Notification
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public bool IsRead { get; set; }
        public int RecipientId { get; set; }
        public User Recipient { get; set; }
        public User Sender { get; set; }
        public int SenderId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
