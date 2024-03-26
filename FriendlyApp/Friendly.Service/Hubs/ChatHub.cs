using Friendly.Database;
using Friendly.Service;
using Friendly.Service.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

[Authorize]
public class ChatHub : Hub<IChatHubClient>
{
    private readonly IConnectionService<string> _connectionService;

    public ChatHub(IConnectionService<string> connectionService)
    {
        _connectionService = connectionService;
    }

    public async Task SendMessageAsync(string message)
    {
        var senderId = Context.User.FindFirst("userid").Value.ToString();
        var recipientId = Context.GetHttpContext().Request.Query["recipient_id"];

        string key = recipientId + "_" + senderId;


        var connections = _connectionService.GetConnections(key);

       
        if (!string.IsNullOrEmpty(message.Trim()))
        {
            string filteredMessage = Regex.Replace(message, @"<.*?>", string.Empty);

            if (connections is null || !connections.Any())
            {
                await Clients.Caller.SendMessageAsync(filteredMessage, true);
                return;
            }

            foreach (var connectionId in _connectionService.GetConnections(key))
            {
                await Clients.Client(connectionId).SendMessageAsync(filteredMessage, false);
            }

            await Clients.Caller.SendMessageAsync(filteredMessage, true);
        }
    }

    public override Task OnConnectedAsync()
    {
        var userId = Context.User.FindFirst("userid").Value.ToString();
        var recipientId = Context.GetHttpContext().Request.Query["recipient_id"];

        _connectionService.AddConnection(userId + "_" + recipientId, Context.ConnectionId);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User.FindFirst("userid").Value.ToString();
        var recipientId = Context.GetHttpContext().Request.Query["recipient_id"];

        _connectionService.RemoveConnection( userId + "_" + recipientId, Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}