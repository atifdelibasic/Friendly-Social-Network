using Friendly.Model.SearchObjects;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BlockController : ControllerBase
{
    private readonly IBlockService _blockService;

    public BlockController(IBlockService blockService)
    {
        _blockService = blockService;
    }

    [HttpPost("block-user/{blockedUserId}")]
    public async Task<IActionResult> BlockUser(int blockedUserId)
    {
        await _blockService.BlockUser(blockedUserId);
        return Ok();
    }

    [HttpPost("unblock-user/{blockedUserId}")]
    public async Task<IActionResult> UnblockUser(int blockedUserId)
    {
        await _blockService.UnblockUser(blockedUserId);
        return Ok();
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetBlockedUsers([FromQuery]SearchBlockedUsersRequest request)
    {
        var blockedUsers = await _blockService.GetBlockedUsers(request);
        return Ok(blockedUsers);
    }
}
