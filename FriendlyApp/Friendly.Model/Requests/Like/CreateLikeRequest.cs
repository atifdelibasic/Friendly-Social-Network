
using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Like
{
    public class CreateLikeRequest
    {
        [Required]
        public int PostId { get; set; }

    }
}
