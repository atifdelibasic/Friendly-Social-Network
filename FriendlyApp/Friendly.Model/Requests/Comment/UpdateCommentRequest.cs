using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Comment
{
    public class UpdateCommentRequest
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Text must be between 1 and 100 characters.")]
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                // Trim leading and trailing spaces from the input string
                _text = value?.Trim();
            }
        }
    }
}
