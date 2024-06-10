using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Model.Requests.Feedback
{
    public class CreateFeedbackRequest
    {
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
