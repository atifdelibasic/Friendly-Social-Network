using Friendly.Model.Requests.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Service
{
    public interface INotificationService
    {
        Task CreateNotification(CreateNotificationRequest request);
        Task<List<Model.Notification>> GetNotifications(GetNotificationsRequest request);
    }
}
