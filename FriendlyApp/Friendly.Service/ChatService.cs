using AutoMapper;
using Friendly.Database;
using Friendly.Model.Requests.Message;
using Friendly.Model.SearchObjects;
using Microsoft.EntityFrameworkCore;

namespace Friendly.Service
{
    public class ChatService : IChatService
    {
        private readonly FriendlyContext _context;
        private readonly HttpAccessorHelperService _httpAccessorHelper;
        private readonly IMapper _mapper;

        public ChatService(FriendlyContext context, IMapper mapper, HttpAccessorHelperService httpAccessorHelper)
        {
            _context = context;
            _httpAccessorHelper = httpAccessorHelper;
            _mapper = mapper;
        }

        public async Task<List<Model.Message>> Get(SearchMessagesRequest request)
        {
            int userId = _httpAccessorHelper.GetUserId();

            var query = _context.Message.Where(x => x.SenderId == userId && x.RecipientId == request.UserId);

            if (request.Cursor.HasValue)
            {
                query = query.Where(x => x.Id < request.Cursor);
            }

            query = query
               .OrderBy(x => x.Id)
               .Take(request.Limit);

            var messages = await query.ToListAsync();

            return _mapper.Map<List<Model.Message>>(messages);
        }

        public async Task<Model.Message> StoreMessage(SendMessageRequest request)
        {
            int userId = _httpAccessorHelper.GetUserId();
            var set = _context.Set<Database.Message>();

            Database.Message entity = _mapper.Map<Database.Message>(request);
            entity.SenderId = userId;

            set.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<Model.Message>(entity);
        }
    }
}
