using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Hobby> Hobby { get; set; }
        public DbSet<HobbyCategory> HobbyCategory { get; set; }
        public DbSet<UserHobby> UserHobbies { get; set; }

    }
}
