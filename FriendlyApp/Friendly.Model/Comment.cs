﻿
namespace Friendly.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
