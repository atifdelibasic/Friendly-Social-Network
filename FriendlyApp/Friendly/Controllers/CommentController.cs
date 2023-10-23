using Friendly.Model;
using Friendly.Model.Requests.Comment;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class CommentController : BaseCRUDController<Comment, SearchCommentRequest, CreateCommentRequest, UpdateCommentRequest>
    {
        private readonly ICommentService _service;
        public CommentController(ICommentService service):base(service)
        {
            _service = service;
        }

        public override async Task<Comment> Insert([FromBody] CreateCommentRequest request)
        {
            var userId = Convert.ToInt32(User.FindFirst("userid").Value);
            request.UserId = userId;

            return await base.Insert(request);
        }

        public override async Task<IEnumerable<Comment>> Get([FromQuery]SearchCommentRequest search = null)
        {
            return await base.Get(search);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _service.DeleteComment(id);

            return Ok(result);
        }
    }
}
