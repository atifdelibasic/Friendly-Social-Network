
namespace Friendly.Model.Requests.Post
{
    public class SearchNearbyPostsRequest
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Limit { get; set; }
        public int? Cursor { get; set; }
    }
}
