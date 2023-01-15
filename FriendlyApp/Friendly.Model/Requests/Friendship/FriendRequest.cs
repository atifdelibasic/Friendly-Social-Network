﻿using System;
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

    public enum Status
    {
        Accepted = 1,
        Declined
    }
}
