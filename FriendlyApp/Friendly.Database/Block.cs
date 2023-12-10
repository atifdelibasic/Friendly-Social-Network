namespace Friendly.Database
{
    public class Block
    {
        public int Id { get; set; }
        public int BlockerUserId { get; set; } 
        public int BlockedUserId { get; set; } 

        // Navigation properties
        public User BlockerUser { get; set; }
        public User BlockedUser { get; set; }
    }
}
