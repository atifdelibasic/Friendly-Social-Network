using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Feedback
{
    public class CreateFeedbackRequest
    {
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Text { get; set; }
    }
}
