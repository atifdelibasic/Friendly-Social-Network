using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Model.Requests.Post
{
    public class CreatePostRequest
    {
        public string UserId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int HobbyId { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

    }
}
