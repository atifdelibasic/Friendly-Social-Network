
namespace Friendly.Model.Requests.Notification
{
    public class CreateNotificationRequest
    {
        public string Message { get; set; }
        public int RecipientId { get; set; }
        public int SenderId { get; set; }

    }
}
