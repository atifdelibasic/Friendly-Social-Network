using Friendly.Model.Requests.Post;
using Friendly.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : BaseCRUDController<Model.Post, object, CreatePostRequest, object>
    {
        public PostController(IPostService service) : base(service)
        {

        }

        public override Model.Post Insert([FromBody] CreatePostRequest request)
        {
            string userId = User.FindFirst("userid")?.Value;
            request.UserId = userId;

            return base.Insert(request);
        }
    }
}
