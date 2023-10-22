using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Model.Requests.Comment
{
    public class SearchCommentRequest
    {
        public int? CommentId { get; set; }
        public int PostId { get; set; }
        public int Limit { get; set; }
        public int Cursor { get; set; }
    }
}
