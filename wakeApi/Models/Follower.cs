﻿using MessagePack;
using wakeApi.Identity;

namespace wakeApi.Models
{
    public class Follower
    {
        public int Id { get; set; }
        public string FollowerName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
