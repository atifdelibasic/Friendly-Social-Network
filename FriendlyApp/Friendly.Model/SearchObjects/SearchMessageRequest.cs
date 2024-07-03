
namespace Friendly.Model.SearchObjects
{
    public class SearchMessageRequest : BaseCursorSearchObject
    {
        public string Text { get; set; }
        public int RecipientId { get; set; }
    }
}
