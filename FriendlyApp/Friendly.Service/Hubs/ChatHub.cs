using Friendly.Database;
using Friendly.Service.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub<IChatHubClient>
{

    [Authorize]
    public override Task OnConnectedAsync()
    {
       var userId = Context.User.FindFirst("userid");

        Console.WriteLine(userId);

        //_connections.Add(name, Context.ConnectionId);

        return base.OnConnectedAsync();
    }
}