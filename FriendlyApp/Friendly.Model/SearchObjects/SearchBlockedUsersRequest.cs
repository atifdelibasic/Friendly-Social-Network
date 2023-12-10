namespace Friendly.Model.SearchObjects
{
    public class SearchBlockedUsersRequest :BaseCursorSearchObject
    {
        public string Text { get; set; }
    }
}
