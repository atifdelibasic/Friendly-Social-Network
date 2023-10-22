using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Comment
{
    public class UpdateCommentRequest
    {
        [Required(ErrorMessage = "CommentId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The selected comment is not valid.")]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Text must be between 1 and 100 characters.")]
        public string Text { get; set; }
    }
}
