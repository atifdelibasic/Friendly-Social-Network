using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Comment
{
    public class CreateCommentRequest
    {
        [Required]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        [StringLength(3000, MinimumLength = 1, ErrorMessage = "Text must be between 1 and 1250 characters.")]
        public string Text { get; set; }

    }
}
