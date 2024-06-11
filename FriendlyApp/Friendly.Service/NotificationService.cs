using AutoMapper;
using Friendly.Database;
using Friendly.Model;
using Friendly.Model.Requests.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly FriendlyContext _context;
        public NotificationService(Database.FriendlyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateNotification(CreateNotificationRequest request)
        {
            Database.Notification n = new Database.Notification
            {
                Message = request.Message,
                RecipientId = request.RecipientId,
                SenderId = request.SenderId
            };

            await _context.Notification.AddAsync(n);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Model.Notification>> GetNotifications(GetNotificationsRequest request)
        {
            List<Database.Notification> notifications = await _context.Notification
                .Include(x => x.Recipient)
                .Include(x => x.Sender)
                .Where(x => x.RecipientId == request.UserId)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return _mapper.Map<List<Model.Notification>>(notifications);
        }
    }
}
