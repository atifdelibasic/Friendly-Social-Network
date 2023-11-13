using Friendly.Database;
using Friendly.Service.Hubs;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub<IChatHubClient>
{
    public async Task SendMessage(int senderId, int receiverId, string content)
    {
        // Broadcast the message to all clients
        await Clients.All.SendMessageAsync("radiiii");
    }
}