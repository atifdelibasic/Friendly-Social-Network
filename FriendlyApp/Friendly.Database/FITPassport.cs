using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Friendly.Database
{
    public class FITPassport : SoftDelete
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public User User { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool isActive { get; set; }
    }
}
