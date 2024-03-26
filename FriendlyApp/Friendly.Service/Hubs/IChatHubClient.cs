

namespace Friendly.Service.Hubs
{
    public interface IChatHubClient
    {
        Task SendMessageAsync(string message, bool isMe);
    }
}
