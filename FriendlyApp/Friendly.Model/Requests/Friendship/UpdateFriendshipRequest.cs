using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Model.Requests.Friendship
{
    public class UpdateFriendshipRequest
    {
        public int Id { get; set; }
        public FriendshipStatus Status { get; set; }
    }
}
