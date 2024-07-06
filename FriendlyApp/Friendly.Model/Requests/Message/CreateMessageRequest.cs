using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Message
{
    public class CreateMessageRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }
        public int RecipientId { get; set; }
        public int SenderId { get; set; }
    }
}
