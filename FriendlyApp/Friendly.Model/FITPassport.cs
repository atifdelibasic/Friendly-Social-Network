using System.Threading.Tasks;

namespace Friendly.Model
{
    public class FITPassport
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool isActive { get; set; }
    }
}
