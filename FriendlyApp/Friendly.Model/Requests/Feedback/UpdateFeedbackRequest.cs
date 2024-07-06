using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Model.Requests.Feedback
{
    public class UpdateFeedbackRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Text { get; set; }
    }
}
