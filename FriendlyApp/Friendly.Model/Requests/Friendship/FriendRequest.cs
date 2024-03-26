using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Model.Requests.Friendship
{
    public class FriendRequest
    {
        public Status Status { get; set; }
    }

    public class FriendshipStatus
    {
        public FriendshipStatus Status { get; set; }
    }

    public enum Status
    {
        Accepted = 1,
        Declined
    }

    public enum fStatus
    {
        Pending = 1,
        Friends,
        Block,
    }
}
