﻿using System.ComponentModel.DataAnnotations;

namespace Friendly.Model.Requests.Hobby
{
    public class UpdateHobbyRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}
