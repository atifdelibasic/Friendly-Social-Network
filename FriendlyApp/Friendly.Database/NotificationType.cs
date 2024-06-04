using System.ComponentModel.DataAnnotations;

namespace Friendly.Database
{
    public class NotificationType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Notification> Notifications { get; set; }
    }
}
