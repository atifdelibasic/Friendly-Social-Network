using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Friendly.Database
{
    public class Friendship : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int FriendId { get; set; }
        [ForeignKey("FriendId")]
        public User Friend { get; set; }

        public FriendshipStatus Status { get; set; }
        public DateTime? DeletedAt { get; set; }
    }


    public enum FriendshipStatus
    {
        Pending = 1,
        Friends,
        Block,
    }
}
