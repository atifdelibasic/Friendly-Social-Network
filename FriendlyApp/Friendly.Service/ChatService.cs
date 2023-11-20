using Friendly.Database;
using Friendly.Service.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Friendly.Service
{
    public class ChatService : IChatService
    {
        private readonly FriendlyContext _context;
        private readonly IHubContext<ChatHub, IChatHubClient> _messageHub;
        private readonly IConnectionService<string> _connectionService;
        private readonly HttpAccessorHelperService _httpAccessorHelper;

        public ChatService(FriendlyContext context, IHubContext<ChatHub, IChatHubClient> messageHub, IConnectionService<string> connectionService, HttpAccessorHelperService httpAccessorHelper)
        {
            _context = context;
            _messageHub = messageHub;
            _connectionService = connectionService;
            _httpAccessorHelper = httpAccessorHelper;
        }

        public void SendMessage(int recipientId, string message)
        {
            int userId = _httpAccessorHelper.GetUserId();

            /*var message = new Message
            {
                SenderId = senderId,
                RecieverId = receiverId,
                Content = content,
                Timestamp = DateTime.Now
            };*/

            //_context.Messages.Add(message);
            //_context.SaveChanges();

            // Notify clients using SignalR
            //_messageHub.Clients.All.SendMessageAsync("test");

            string key = userId.ToString();


            foreach (var connectionId in _connectionService.GetConnections(key))
            {
                _messageHub.Clients.Client(connectionId).SendMessageAsync(message);
            }

            Console.WriteLine("poruka uspjesno poslana");
        }
    }
}
