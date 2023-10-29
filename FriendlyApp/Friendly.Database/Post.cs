using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Friendly.Database
{
    public class Post : SoftDelete
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public double? Longitude { get; set; } 
        public double? Latitude { get; set; }

        public int HobbyId { get; set; }
        [ForeignKey("HobbyId")]
        public Hobby Hobby { get; set; }

        public string Description { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
