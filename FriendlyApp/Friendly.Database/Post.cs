using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Database
{
    public class Post : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public int HobbyId { get; set; }
        [ForeignKey("HobbyId")]
        public Hobby Hobby { get; set; }

        public string Description { get; set; }
        public string? ImagePath { get; set; }
    }
}
