using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Friendly.Service
{
    public class HttpAccessorHelperService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpAccessorHelperService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("userid");

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            throw new InvalidOperationException("User ID claim is not present or not a valid integer.");
        }
    }
}
