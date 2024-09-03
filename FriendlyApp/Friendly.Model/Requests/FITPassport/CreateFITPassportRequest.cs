
namespace Friendly.Model.Requests.FITPassport
{
    public class CreateFITPassportRequest
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool isActive { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
