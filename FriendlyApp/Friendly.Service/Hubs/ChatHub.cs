using Friendly.Database;
using Friendly.Service;
using Friendly.Service.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;


public class ChatHub : Hub<IChatHubClient>
{
    private readonly IConnectionService<string> _connectionService;

    public ChatHub(IConnectionService<string> connectionService)
    {
        _connectionService = connectionService;
    }

    public override Task OnConnectedAsync()
    {
        var userId = Context.User.FindFirst("userid").Value.ToString();

        _connectionService.AddConnection(userId, Context.ConnectionId);

        //_connections.Add(name, Context.ConnectionId);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User.FindFirst("userid").Value.ToString();

        _connectionService.RemoveConnection(userId, Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}