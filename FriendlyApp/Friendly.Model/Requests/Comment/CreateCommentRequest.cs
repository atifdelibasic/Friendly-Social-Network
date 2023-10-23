using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Comment
{
    public class CreateCommentRequest
    {
        [Required(ErrorMessage = "PostId is required.")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Text must be between 1 and 250 characters.")]
        public string Text { get; set; }

        public int UserId { get; set; }
    }
}
