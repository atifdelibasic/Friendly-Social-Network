using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Friendly.Database
{
    public class Like : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int? PostId { get; set; }
        [ForeignKey("PostId")]
        public Post? Post { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int? CommentId { get; set; }
        [ForeignKey("CommentId")]
        public Comment? Comment { get; set; }
    }
}
