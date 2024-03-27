using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Model.SearchObjects
{
    public class SearchUserCursorRequest : BaseCursorSearchObject
    {
        public string Text { get; set; }
    }
}
