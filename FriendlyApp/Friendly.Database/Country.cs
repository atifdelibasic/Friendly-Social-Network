using System.ComponentModel.DataAnnotations;

namespace Friendly.Database
{
    public class Country : SoftDelete
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
