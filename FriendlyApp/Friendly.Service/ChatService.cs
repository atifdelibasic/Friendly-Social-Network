using Friendly.Database;
using Friendly.Service.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Friendly.Service
{
    public class ChatService : IChatService
    {
        private readonly FriendlyContext _context;
        private readonly IHubContext<ChatHub, IChatHubClient> _messageHub;

        public ChatService(FriendlyContext context, IHubContext<ChatHub, IChatHubClient> messageHub)
        {
            _context = context;
            _messageHub = messageHub;
        }

        public void SendMessage(int senderId, int receiverId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                RecieverId = receiverId,
                Content = content,
                Timestamp = DateTime.Now
            };

            //_context.Messages.Add(message);
            //_context.SaveChanges();

            // Notify clients using SignalR
            _messageHub.Clients.All.SendMessageAsync("test");
            Console.WriteLine("poruka uspjesno poslana");
        }
    }
}
