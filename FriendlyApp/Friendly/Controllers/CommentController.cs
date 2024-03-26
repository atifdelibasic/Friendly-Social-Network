using Friendly.Model;
using Friendly.Model.Requests.Comment;
using Friendly.Model.SearchObjects;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _service.DeleteComment(id);

            return Ok(result);
        }

        [HttpGet("cursor")]
        public async Task<IActionResult> GetCommentsCursor([FromQuery]SearchCommentCursorRequest request)
        {
            var comments = await _service.GetCommentsCursor(request);

            return Ok(comments);
        }
    }
}
