
namespace Friendly.Model
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public Model.User User { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
