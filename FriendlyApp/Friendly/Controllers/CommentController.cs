using Friendly.Model;
using Friendly.Model.Requests.Comment;
using Friendly.Model.Requests.Hobby;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class CommentController : BaseCRUDController<Model.Comment, SearchCommentRequest, CreateCommentRequest, UpdateCommentRequest>
    {
        public CommentController(ICommentService service):base(service)
        {
           
        }

        public override Comment Insert([FromBody] CreateCommentRequest request)
        {
            var userId = Convert.ToInt32(User.FindFirst("userid").Value);
            request.UserId = userId;

            return base.Insert(request);
        }

        public override async Task<IEnumerable<Comment>> Get([FromQuery]SearchCommentRequest search = null)
        {
            return await base.Get(search);
        }
    }
}
