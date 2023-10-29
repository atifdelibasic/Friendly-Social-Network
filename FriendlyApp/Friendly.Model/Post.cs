﻿
namespace Friendly.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public User User { get; set; }
        public Hobby Hobby { get; set; }
    }
}
