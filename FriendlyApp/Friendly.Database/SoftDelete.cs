
namespace Friendly.Database
{
    public class SoftDelete : BaseEntity
    {
        public DateTime? DeletedAt { get; set; }
    }
}
