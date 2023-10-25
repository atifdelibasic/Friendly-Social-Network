using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Model.Requests.Like
{
    public class SearchLikesRequest
    {
        [Required]
        public int PostId { get; set; }

        [Range(1, 100, ErrorMessage = "Limit must be between 1 and 100.")]
        public int Limit { get; set; } = 50;
        public int? Cursor { get; set; } = null;
    }
}
