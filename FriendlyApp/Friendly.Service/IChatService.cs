using Friendly.Model.Requests.Message;

namespace Friendly.Service
{
    public interface IChatService
    {
        public Task<Model.Message> StoreMessage(SendMessageRequest request);
    }
}
