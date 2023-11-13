using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    [Route("send")]
    public string SendMessage()
    {

        _chatService.SendMessage(1, 1, "poruka");

        // Perform additional logic if needed

        return "done";
    }
}
