
namespace Friendly.Model
{
    public class City
    {
        public Country Country { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
