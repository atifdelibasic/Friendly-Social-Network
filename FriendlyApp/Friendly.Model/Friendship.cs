using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Model
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public User Friend { get; set; }

        public FriendshipStatus Status { get; set; }
    }
    public enum FriendshipStatus
    {
        Pending = 1,
        Friends,
        Block,
    }
}
