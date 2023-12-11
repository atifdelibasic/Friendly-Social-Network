using Friendly.Model.Requests.Message;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    [Route("store")]
    public async Task<IActionResult> StoreMessage(SendMessageRequest request)
    {
       var result = await _chatService.StoreMessage(request);

        return Ok(result);
    }

   

}
