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
    }
}
