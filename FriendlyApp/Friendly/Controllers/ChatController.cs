using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    [Route("send")]
    public string SendMessage(int recipientId, string message)
    {

        _chatService.SendMessage(recipientId, message );

        // Perform additional logic if needed

        return "done";
    }
}
