using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Database
{
    public class FriendlyContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public FriendlyContext(DbContextOptions<FriendlyContext> options) :base(options)
        {
              
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserHobby>().HasKey(sc => new { sc.UserId, sc.HobbyId });
            modelBuilder.Entity<Friendship>().HasQueryFilter(x => x.DeletedAt == null);
            modelBuilder.Entity<Post>().HasQueryFilter(x => x.DeletedAt == null);
            modelBuilder.Entity<Comment>().HasQueryFilter(x => x.DeletedAt == null);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Hobby> Hobby { get; set; }
        public DbSet<HobbyCategory> HobbyCategory { get; set; }
        public DbSet<UserHobby> UserHobbies { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Friendship> Friendship { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Block> Block { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportReason> ReportReason { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<NotificationType> NotificationType { get; set; }
    }
}
