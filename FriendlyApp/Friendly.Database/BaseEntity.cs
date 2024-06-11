
namespace Friendly.Database
{
    public class BaseEntity
    {
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
    }
}
